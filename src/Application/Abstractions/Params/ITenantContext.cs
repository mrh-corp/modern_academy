using Application.Abstractions.Service;
using Domain.Academies;

namespace Application.Abstractions.Params;

public interface ITenantContext : IService
{
    string? TenantName { get; set; }
    Academy? Academy { get; set; }
}
