using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Nodes;
using Domain.Registrations;
using SharedKernel;

namespace Domain.Students;

public class Student : Entity
{
    public string FullName { get; set; }
    public DateOnly BirthDate { get; set; }
    public string BirthPlace { get; set; }
    public string? Email { get; set; }
    public char Gender { get; set; }
    public string? Contact { get; set; }
    public string CurrentAddress { get; set; }
    public string FatherName { get; set; }
    public string MotherName { get; set; }
    public string FatherJob { get; set; }
    public string MotherJob { get; set; }
    public string FatherContact { get; set; }
    public string MotherContact { get; set; }
    public string? TutorName { get; set; }
    public string? TutorContact { get; set; }
    public string?  CustomFields { get; set; }
    public virtual IEnumerable<Registration>? Registrations { get; set; }

    [NotMapped]
    public JsonObject Data
    {
        get => string.IsNullOrEmpty(CustomFields) ? new JsonObject() : JsonNode.Parse(CustomFields)!.AsObject();
        set => CustomFields = value?.ToJsonString();
    }
}
