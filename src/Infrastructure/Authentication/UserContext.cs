using Application.Abstractions.Authentication;
using Application.Users;
using Domain.Users;
using Microsoft.AspNetCore.Http;
using SharedKernel;

namespace Infrastructure.Authentication;

internal sealed class UserContext(
    IHttpContextAccessor httpContextAccessor,
    IUserRepository userRepository)
    : IUserContext
{
    public Guid UserId =>
        httpContextAccessor
            .HttpContext?
            .User
            .GetUserId() ??
        throw new ApplicationException("User context is unavailable");

    public Task<User> CurrentUser => GetCurrentUser();

    private async Task<User> GetCurrentUser()
    {
        Result<User> resultUser = await userRepository.GetUser(UserId);
        return resultUser.IsSuccess ? resultUser.Value : throw new ApplicationException("User not found");
    }
}
