using Domain.Academies;

namespace Application.Academies;

public sealed record AcademyDto(string Name, string Description, string Email, string Contact);
public sealed record SchoolYearDto(Guid AcademyId, DateOnly StartDate, DateOnly EndDate);

public sealed record ClassDto(Guid TempId, string Name, string Label, ClassSection Section, Guid? PreviousId, Guid? NextId);
