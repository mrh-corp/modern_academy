using SharedKernel;

namespace Domain.Students;

public static class StudentError
{
    public static Error DuplicateName(string name) =>
        Error.Conflict("Student.Duplication", "Student with the same name already exists."); 
    
}
