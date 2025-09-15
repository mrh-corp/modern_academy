using System.Text.Json.Nodes;
using Domain.Academies;
using SharedKernel;

namespace Domain.Fields;

public class CustomField : Entity
{
    public JsonObject Fields { get; set; }
    public Academy Academy { get; set; }
    public CustomFieldFor CustomFieldFor { get; set; }
}
