using SharedKernel;

namespace Domain.Courses;

public class Course : Entity
{
    public string Name { get; set; }
    public string Label { get; set; }
    public string Description { get; set; }
}
