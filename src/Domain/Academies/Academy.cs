using Domain.Users;
using SharedKernel;

namespace Domain.Academies;

public sealed class Academy : Entity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Email { get; set; }
    public string Contact { get; set; }
    public List<User> Administrators { get; set; }
    public List<SchoolYear> SchoolYears { get; set; }
}
