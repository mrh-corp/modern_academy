using Domain.Academies;
using SharedKernel;

namespace Domain.Courses;

public class ClassCourse : Entity
{
    public Guid AcademyId { get; set; }
    public Class Class { get; set; }
    public List<Course> Courses { get; set; }
    public Guid SchoolYearId { get; set; }
}
