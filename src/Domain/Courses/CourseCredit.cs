using Domain.Academies;
using SharedKernel;

namespace Domain.Courses;

public class CourseCredit : Entity
{
    public double Credit { get; set; }
    
    public Guid CourseId { get; set; }
    public virtual Course Course { get; set; }
    
    public Guid SchoolYearId { get; set; }
    public virtual SchoolYear SchoolYear { get; set; }
}
