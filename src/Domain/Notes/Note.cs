using System.ComponentModel.DataAnnotations.Schema;
using Domain.Courses;
using Domain.Students;
using SharedKernel;

namespace Domain.Notes;

public class Note : Entity
{
    public double Value { get; set; }
    
    public Guid StudentId { get; set; }
    public virtual Student Student { get; set; }
    
    public Guid CourseId { get; set; }
    public virtual Course Course { get; set; }

    [NotMapped]
    public double ValueWithCredit
    {
        get
        {
            if (Course.CourseCredit != null)
            {
                return Value * Course.CourseCredit.Credit;
            }
            return 0;
        }
    }
    
    public Guid TestId { get; set; }
    public virtual Test Test { get; set; }
}
