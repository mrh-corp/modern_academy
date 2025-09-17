using Domain.Academies;
using SharedKernel;

namespace Domain.Users;

public sealed class User : Entity
{
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string? LastName { get; set; }
    public string PasswordHash { get; set; }
    public List<Academy>  Academies { get; set; }
}
