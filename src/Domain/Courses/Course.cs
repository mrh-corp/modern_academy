using SharedKernel;

namespace Domain.Courses;

public class Course : Entity
{
    public string Name { get; set; }
    public string Label { get; set; }
    public string Description { get; set; }
    public virtual IEnumerable<ClassCourse>? ClassCourses { get; set; }
    public virtual CourseCredit? CourseCredit { get; set; }
    public virtual IEnumerable<CourseCredit>? CourseCredits { get; set; }
}
