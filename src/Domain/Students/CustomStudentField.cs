using System.Text.Json;
using Domain.Academies;
using SharedKernel;

namespace Domain.Students;

public class CustomStudentField : Entity
{
    public Academy Academy { get; set; }
    public string Name { get; set; }
    public string Label { get; set; }
    public string Type  { get; set; }
    public string FieldValue { get; set; }

    public T GetValue<T>() => JsonSerializer.Deserialize<T>(FieldValue);
}
