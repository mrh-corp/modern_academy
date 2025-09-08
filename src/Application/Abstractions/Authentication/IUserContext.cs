using Domain.Users;

namespace Application.Abstractions.Authentication;

public interface IUserContext
{
    Guid UserId { get; }
    Task<User> CurrentUser { get; }
}
