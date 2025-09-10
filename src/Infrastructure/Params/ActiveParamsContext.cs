using Application.Abstractions.Params;
using Domain.Academies;

namespace Infrastructure.Params;

public class ActiveParamsContext :  IActiveParamsContext
{
    public Guid SchoolYearId { get; set; }
    public Task<SchoolYear> ActiveSchoolYear { get; set; }
    public Guid AcademyId { get; set; }
    public Task<Academy> ActiveAcademy { get; set; }
}
