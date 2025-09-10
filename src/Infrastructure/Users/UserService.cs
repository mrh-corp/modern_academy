using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Users;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Infrastructure.Users;

public class UserService(
    IApplicationDbContext context,
    IPasswordHasher hasher) : IUserRepository
{

    public async Task<Result<User>> AddUser(CreateUserDto dto)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            FirstName = dto.Firstname,
            LastName = dto.Lastname,
            PasswordHash = hasher.Hash(dto.Password),
            Email = dto.Email,
            
        };
        user.Raise(new UserRegisteredDomainEvent(user.Id));
        context.Users.Add(user);
        await context.SaveChangesAsync();
        
        return Result.Success<User>(user);
    }

    public async Task<Result<User>> GetUser(Guid id)
    {
        User user = await context.Users.SingleOrDefaultAsync(u => u.Id == id);
        return user;
    }

    public async Task<Result<User>> GetUser(string id)
    {
        User user = await context.Users.SingleOrDefaultAsync(u => u.Id == Guid.Parse(id));
        return user;
    }

    public async Task<User?> GetUserByUsername(string username)
    {
        User? user = await context.Users.SingleOrDefaultAsync(u => u.Email == username);
        return user;
    }

    public Task<bool> VerifyIfUserMatchPassword(User user, string password)
    {
        bool passwordMatch = hasher.Verify(password, user.PasswordHash);
        return Task.FromResult(passwordMatch);
    }
}
