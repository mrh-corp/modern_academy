using SharedKernel;

namespace Domain.Courses;

public class CourseCredit : Entity
{
    public double Credit { get; set; }
    public Course Course { get; set; }
}
