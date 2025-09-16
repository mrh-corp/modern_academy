using Application.Abstractions.Params;
using Domain.Academies;

namespace Infrastructure.Params;

public class TenantContext : ITenantContext
{
    public string? TenantName { get; set; }
    public Academy? Academy { get; set; }
}
