using Application.Abstractions.Service;
using Application.Users.Dtos;
using Domain.Users;
using SharedKernel;

namespace Application.Users.Service;

public interface IUserRepository : IService
{
    Task<Result<User>> AddUser(CreateUserDto dto);
}
