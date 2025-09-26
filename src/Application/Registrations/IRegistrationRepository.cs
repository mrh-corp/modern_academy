using Application.Abstractions.Service;
using Domain.Registrations;
using Domain.Students;
using OneOf;
using SharedKernel;

namespace Application.Registrations;

public interface IRegistrationRepository : IService
{
    Task<bool> GetStudentByName(string name);
    Task<OneOf<Error, Student>> AddStudent(RegisterStudentDto registerStudentDto);
}
