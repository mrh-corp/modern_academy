using Domain.Academies;
using Domain.Students;
using SharedKernel;

namespace Domain.Registrations;

public class Registration : Entity
{
    public Guid StudentId { get; set; }
    public virtual Student Student { get; set; }
    
    public Guid AcademyId { get; set; }
    public virtual Academy  Academy { get; set; }
    
    public Guid CurrentSchoolYearId { get; set; }
    public virtual SchoolYear SchoolYear { get; set; }
    
    public Guid CurrentClassId { get; set; }
    public virtual Class CurrentClass { get; set; }
}
