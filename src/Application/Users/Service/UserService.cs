using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Users.Dtos;
using Domain.Users;
using SharedKernel;

namespace Application.Users.Service;

public class UserService(
    IApplicationDbContext context,
    IPasswordHasher hasher) : IUserRepository
{

    public async Task<Result<User>> AddUser(CreateUserDto dto)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            FirstName = "test",
            LastName = "test",
            PasswordHash = hasher.Hash(dto.password),
            Email = "test@test.com",
            
        };
        user.Raise(new UserRegisteredDomainEvent(user.Id));
        context.Users.Add(user);
        await context.SaveChangesAsync();
        
        return Result.Success<User>(user);
    }
}
