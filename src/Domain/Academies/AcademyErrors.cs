using SharedKernel;

namespace Domain.Academies;

public static class AcademyErrors
{
    public static Error NameNotUnique(string name) => Error.Problem(
        "Academy.NameNotUnique",
        $"Academy with the name {name} already exists.");
}
