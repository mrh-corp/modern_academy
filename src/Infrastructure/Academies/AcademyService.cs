using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Academies;
using Domain.Academies;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using OneOf;
using SharedKernel;

namespace Infrastructure.Academies;

public class AcademyService(
    IUserContext userContext,
    IApplicationDbContext context) : IAcademyRepository
{
    public async Task<OneOf<Error, Academy>> CreateAcademy(AcademyDto academyDto)
    {
        bool nameNotExists = await context.Academies.AnyAsync(x => x.Name == academyDto.Name);
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
        await context.SaveChangesAsync();
        return academy;
    }

    public Task<List<Academy>> GetAllAcademy()
    {
        throw new NotImplementedException();
    }
}
