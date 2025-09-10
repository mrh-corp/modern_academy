using System.ComponentModel.DataAnnotations.Schema;
using SharedKernel;

namespace Domain.Academies;

public class SchoolYear : Entity
{
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }

    [NotMapped]
    public string Label => $"{StartDate.Year}-{EndDate.Year}";
}
