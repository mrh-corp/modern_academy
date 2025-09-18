using System.Text.Json.Nodes;

namespace Application.Students;

public record RegisterStudentDto(
    string FullName,
    DateOnly BirthDate,
    string BirthPlace,
    string? Email,
    string? Contact,
    char Gender,
    string CurrentAddress,
    string FatherName,
    string MotherName,
    string FatherJob,
    string MotherJob,
    string FatherContact,
    string MotherContact,
    string? TutorName,
    string? TutorContact,
    JsonObject?  CustomFields,
    Guid ClassUid,
    Guid PreviousClassUid
    );
