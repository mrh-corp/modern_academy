using System.Text.Json.Nodes;
using SharedKernel;

namespace Domain.Students;

public class Student : Entity
{
    public string RegistrationNumber { get; set; }
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
    public string TutorContact { get; set; }
    public JsonObject?  CustomFields { get; set; }
}
