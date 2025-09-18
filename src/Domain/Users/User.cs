using Domain.Academies;
using SharedKernel;

namespace Domain.Users;

public class User : Entity
{
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string? LastName { get; set; }
    public string PasswordHash { get; set; }
    public virtual List<Academy>  Academies { get; set; }
}
