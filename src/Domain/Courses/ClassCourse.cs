using System.ComponentModel.DataAnnotations.Schema;
using Domain.Academies;
using SharedKernel;

namespace Domain.Courses;

public class ClassCourse : Entity
{
    public Guid AcademyId { get; set; }
    public virtual Academy Academy { get; set; }
    
    public Guid ClassId { get; set; }
    public virtual Class Class { get; set; }
    
    public virtual List<Course> Courses { get; set; }

    public Guid SchoolYearId { get; set; }
    public virtual SchoolYear SchoolYear { get; set; }
}
