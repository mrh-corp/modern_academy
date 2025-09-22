using SharedKernel;

namespace Domain.Notes;

public class Test : Entity
{
    public string Name { get; set; }
    public double Percentage { get; set; }
    
    public Guid TrimesterId { get; set; }
    public virtual Trimester Trimester { get; set; }
    
    public virtual IEnumerable<Note> Notes { get; set; }
}
