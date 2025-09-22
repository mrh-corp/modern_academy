using Domain.Academies;
using SharedKernel;

namespace Domain.Notes;

public class Trimester : Entity
{
    public string Name { get; set; }
    public Guid SchoolYearId { get; set; }
    public virtual SchoolYear SchoolYear { get; set; }
    
    public double Percentage { get; set; }
    
    public virtual IEnumerable<Test> Tests { get; set; }
}
