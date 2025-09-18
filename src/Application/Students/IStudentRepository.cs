using Application.Abstractions.Service;
using Domain.Students;
using OneOf;
using SharedKernel;

namespace Application.Students;

public interface IStudentRepository : IService
{
    Task<bool> GetStudentByName(string name);
    Task<OneOf<Error, Student>> AddStudent(RegisterStudentDto registerStudentDto);
}
