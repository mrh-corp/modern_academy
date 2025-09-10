using Application.Abstractions.Service;
using Domain.Users;
using SharedKernel;

namespace Application.Users;

public interface IUserRepository : IService
{
    Task<Result<User>> AddUser(CreateUserDto dto);
    Task<Result<User>> GetUser(Guid id);
    Task<Result<User>> GetUser(string id);
    Task<User?> GetUserByUsername(string username);
    Task<bool> VerifyIfUserMatchPassword(User user, string password);
}
