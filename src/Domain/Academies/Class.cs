using SharedKernel;

namespace Domain.Academies;

public class Class : Entity
{
    public string Name { get; set; }
    public string Label { get; set; }
    
    public Guid? PreviousClassId { get; set; }
    public Class? PreviousClass { get; set; }
    
    public Guid? NextClassId { get; set; }
    public Class? NextClass { get; set; }
    public Academy Academy { get; set; }
    public ClassSection Section { get; set; }
}
