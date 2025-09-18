using System.Text.Json.Nodes;
using Domain.Registrations;
using Domain.Users;
using SharedKernel;

namespace Domain.Academies;

public class Academy : Entity
{
    public string Name { get; set; }
    public string TenantName { get; set; }
    public string Description { get; set; }
    public string Email { get; set; }
    public string Contact { get; set; }
    public string? LogoAttachmentUrl { get; set; }
    public virtual List<User> Administrators { get; set; }
    public virtual List<SchoolYear> SchoolYears { get; set; }
    public virtual IEnumerable<Registration>? Registrations { get; set; }
    public virtual IEnumerable<Class>? Classes { get; set; }
}
