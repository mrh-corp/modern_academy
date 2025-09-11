using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Params;
using Application.Academies;
using Domain.Academies;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using OneOf;
using SharedKernel;

namespace Infrastructure.Academies;

public class AcademyService(
    IUserContext userContext,
    IActiveParamsContext paramsContext,
    IApplicationDbContext context) : IAcademyRepository
{
    public async Task<OneOf<Error, Academy>> CreateAcademy(AcademyDto academyDto, CancellationToken cancellationToken = default)
    {
        bool nameNotExists = await context.Academies.AnyAsync(x => x.Name == academyDto.Name, cancellationToken);
        if (nameNotExists)
        {
            return AcademyErrors.NameNotUnique(academyDto.Name);
        }

        User user = await userContext.CurrentUser;
        var academy = new Academy
        {
            Name = academyDto.Name,
            Description = academyDto.Description,
            Email = academyDto.Email,
            Contact = academyDto.Contact,
            Administrators = [user]
        };
        context.Academies.Add(academy);
        await context.SaveChangesAsync(cancellationToken);
        return academy;
    }

    public Task<List<Academy>> GetAllAcademy()
    {
        throw new NotImplementedException();
    }

    public async Task<OneOf<Error, List<SchoolYear>>> CreateSchoolYear(
        SchoolYearDto schoolYearDto,
        CancellationToken cancellationToken = default)
    {
        await using IDbContextTransaction transaction = await context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            Academy academy = await context.Academies
                .Include(x => x.Administrators)
                .Include(x => x.SchoolYears)
                .AsSplitQuery()
                .SingleOrDefaultAsync(x => x.Id == schoolYearDto.AcademyId, cancellationToken);
            if (academy is null)
            {
                return AcademyErrors.NotFound(schoolYearDto.AcademyId);
            }
            
            bool userIsAdmin = academy.Administrators.Any(x => x.Id == userContext.UserId);
            if (!userIsAdmin)
            {
                return AcademyErrors.Forbidden(schoolYearDto.AcademyId);
            }

            var schoolYear = new SchoolYear
            {
                StartDate = schoolYearDto.StartDate,
                EndDate = schoolYearDto.EndDate
            };
            
            bool schoolYearExists = academy.SchoolYears.Any(x => x.Label == schoolYear.Label);
            if (schoolYearExists)
            {
                return AcademyErrors.SchoolYearConflict(schoolYear.Label);
            }
            context.SchoolYears.Add(schoolYear);
            await context.SaveChangesAsync(cancellationToken);
            academy.SchoolYears ??= [];
            academy.SchoolYears.Add(schoolYear);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
            return academy.SchoolYears;
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync(cancellationToken);
            return Error.Problem("Unknown.Error", e.Message);
        }
    }

    public async Task<OneOf<Error, List<SchoolYear>>> GetAllSchoolYear(
        Guid academyId,
        CancellationToken cancellationToken = default)
    {
        Academy academy = await context.Academies.SingleOrDefaultAsync(x => x.Id == academyId,  cancellationToken);
        if (academy is null)
        {
            return AcademyErrors.NotFound(academyId);
        }
        return academy.SchoolYears;
    }

    public async Task<OneOf<Error, List<Class>>> AddClasses(ClassDto[] classes, CancellationToken cancellationToken = default)
    {
        Academy academy = await paramsContext.ActiveAcademy;
        
        await using IDbContextTransaction transaction = await context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            var tempMap = new Dictionary<Guid, Class>();
            var result = new List<Class>();
            var duplicates = new List<(string Field, string Value)>();

            string[] names = classes.Select(x => x.Name).ToArray();
            string[] labels = classes.Select(x => x.Label).ToArray();

            List<Class> existingConflicts = await context.Classes.Where(c =>
                    c.Academy.Id == academy.Id && (names.Contains(c.Name) || labels.Contains(c.Label)))
                .ToListAsync(cancellationToken);

            foreach (Class conflict in existingConflicts)
            {
                if (names.Contains(conflict.Name))
                {
                    duplicates.Add(("Name", conflict.Name));
                }

                if (labels.Contains(conflict.Label))
                {
                    duplicates.Add(("Label", conflict.Label));
                }
            }
            if (existingConflicts.Any())
            {
                return AcademyErrors.ExistingClasses(duplicates);
            }
            
            foreach (ClassDto classDto in classes)
            {
                var @class = new Class
                {
                    Label = classDto.Label,
                    Name = classDto.Name,
                    Academy = academy,
                    Section = classDto.Section,
                };

                tempMap[classDto.TempId] = @class;
                result.Add(@class);

                context.Classes.Add(@class);
            }
            await context.SaveChangesAsync(cancellationToken);

            foreach (ClassDto classDto in classes)
            {
                Class @class = tempMap[classDto.TempId];

                if (classDto.PreviousId is { } prevId)
                {
                    @class.PreviousClass = tempMap.TryGetValue(prevId, out Class? prevClass)
                        ? prevClass
                        : await context.Classes.SingleOrDefaultAsync(x => x.Id == prevId, cancellationToken);
                }

                if (classDto.NextId is {} nextId)
                {
                    @class.NextClass = tempMap.TryGetValue(nextId, out Class? nextClass)
                        ? nextClass
                        : await context.Classes.SingleOrDefaultAsync(x => x.Id == nextId, cancellationToken);
                }
            }

            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            return result;
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync(cancellationToken);
            return Error.Problem("Unknown.Error", e.Message);
        }
    }
}
