using System.ComponentModel.DataAnnotations.Schema;
using Domain.Courses;
using Domain.Registrations;
using SharedKernel;

namespace Domain.Academies;

public class SchoolYear : Entity
{
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }

    [NotMapped]
    public string Label => $"{StartDate.Year}-{EndDate.Year}";

    public virtual IEnumerable<Registration>? Registrations { get; set; }
    public virtual IEnumerable<ClassCourse>? ClassCourses { get; set; }
}
