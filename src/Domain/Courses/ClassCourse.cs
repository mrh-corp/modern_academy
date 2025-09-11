using Domain.Academies;
using SharedKernel;

namespace Domain.Courses;

public class ClassCourse : Entity
{
    public Academy Academy { get; set; }
    public Class Class { get; set; }
    public List<Course> Courses { get; set; }
    public SchoolYear  SchoolYear { get; set; }
}
