using Application.Abstractions.Data;
using Application.Abstractions.Params;
using Application.Academies;
using Application.Students;
using Domain.Academies;
using Domain.Registrations;
using Domain.Students;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using OneOf;
using SharedKernel;

namespace Infrastructure.Students;

public class StudentService(
    IApplicationDbContext context,
    IClassRepository classRepository,
    ITenantContext tenantContext,
    IActiveParamsContext activeParamsContext) : IStudentRepository
{
    public async Task<bool> GetStudentByName(string name)
    {
        return await context.Students.SingleOrDefaultAsync(x => x.FullName == name) != null;
    }

    public async Task<OneOf<Error, Student>> AddStudent(RegisterStudentDto registerStudentDto)
    {
        bool isNameExist = await GetStudentByName(registerStudentDto.FullName);
        if (isNameExist)
        {
            return StudentError.DuplicateName(registerStudentDto.FullName);
        }

        await using IDbContextTransaction transaction = await context.Database.BeginTransactionAsync();
        try
        {
            var student = new Student
            {
                FullName = registerStudentDto.FullName,
                Email = registerStudentDto.Email,
                BirthDate = registerStudentDto.BirthDate,
                BirthPlace = registerStudentDto.BirthPlace,
                Contact = registerStudentDto.Contact,
                CurrentAddress = registerStudentDto.CurrentAddress,
                FatherName = registerStudentDto.FatherName,
                MotherName = registerStudentDto.MotherName,
                FatherContact = registerStudentDto.FatherContact,
                MotherContact = registerStudentDto.MotherContact,
                FatherJob = registerStudentDto.FatherJob,
                MotherJob = registerStudentDto.MotherJob,
                TutorName = registerStudentDto.TutorName,
                TutorContact = registerStudentDto.TutorContact,
                Gender = registerStudentDto.Gender,
                CustomFields = registerStudentDto.CustomFields?.ToJsonString()
            };

            context.Students.Add(student);
            
            //Register the user in the current school year
            Class currentClass = await classRepository.GetClassById(registerStudentDto.ClassUid)
                                 ?? throw new InvalidOperationException("Class not found");
            var registration = new Registration
            {
                StudentId = student.Id,
                CurrentClassId = currentClass.Id,
                CurrentSchoolYearId = (await activeParamsContext.ActiveSchoolYear).Id,
                AcademyId = tenantContext.Academy!.Id
            };
            context.Registrations.Add(registration);
            await context.SaveChangesAsync();
            await transaction.CommitAsync();
            return student;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}
