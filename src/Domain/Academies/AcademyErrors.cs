using SharedKernel;

namespace Domain.Academies;

public static class AcademyErrors
{
    public static Error NameNotUnique(string name) => Error.Problem(
        "Academy.NameNotUnique",
        $"Academy with the name {name} already exists.");
    
    public static Error NotFound(Guid id) => Error.NotFound(
        "Academy.IdNotFound",
        $"Academy with the id {id} does not exist.");
    
    public static Error Forbidden(Guid id) => Error.Forbidden(
        "Academy.UserNotAdmin",
        $"Academy with the id {id} does not belong to you.");
    
    public static Error SchoolYearConflict(string label) => Error.Conflict(
        "Academy.SchoolYearConflict",
        $"School year {label} conflict.");
}
