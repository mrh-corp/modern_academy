using Domain.Courses;
using Domain.Registrations;
using SharedKernel;

namespace Domain.Academies;

public class Class : Entity
{
    public string Name { get; set; }
    public string Label { get; set; }
    
    public Guid? PreviousClassId { get; set; }
    public virtual Class? PreviousClass { get; set; }
    
    public Guid? NextClassId { get; set; }
    public virtual Class? NextClass { get; set; }
    
    public Guid AcademyId { get; set; }
    public virtual Academy Academy { get; set; }
    public ClassSection Section { get; set; }
    public virtual IEnumerable<Registration>? Registrations { get; set; }
    public virtual IEnumerable<ClassCourse>? Courses { get; set; }
}
