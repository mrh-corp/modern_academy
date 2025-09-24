using Application.Abstractions.Data;
using Application.Abstractions.Params;
using Domain.Academies;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Params;

public class ActiveParamsContext(
    ITenantContext tenantContext) :  IActiveParamsContext
{
    public Guid SchoolYearId { get; set; }
    private SchoolYear? _activeSchoolYear;
    public SchoolYear ActiveSchoolYear
    {
        get
        {
            _activeSchoolYear ??= GetActiveSchoolYear();
            return _activeSchoolYear;
        }
    }

    private SchoolYear? GetActiveSchoolYear()
    {
        SchoolYear schoolYear = tenantContext.Academy!.SchoolYears.SingleOrDefault(s => s.Id == SchoolYearId);
        return schoolYear;
    }
}
