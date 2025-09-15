using Application.Abstractions.Data;
using Application.Abstractions.Params;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using Web.Api.Infrastructure;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Web.Api.Middleware;

public class SubDomainTenantMiddleware(
    RequestDelegate next,
    IConfiguration configuration,
    ITenantContext tenantContext,
    IApplicationDbContext dbContext)
{
    public async Task Invoke(HttpContext context)
    {
        string host = context.Request.Host.Host;
        string? tenant = GetTenantFromHost(host);
        if (tenant != null)
        {
            tenantContext.TenantName = tenant;
            tenantContext.Academy = await dbContext.Academies
                .Include(x => x.Administrators)
                .Include(x => x.SchoolYears)
                .AsSplitQuery()
                .SingleOrDefaultAsync(x => x.TenantName == tenant);
        }

        Endpoint? endpoint = context.GetEndpoint();
        if (endpoint != null)
        {
            bool hasTenantRequired = endpoint.Metadata.GetMetadata<TenantRequiredAttribute>() != null;
            if (hasTenantRequired && (tenant is null || tenantContext.Academy != null))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                var result = Result.Failure(Error.Problem("Tenant.NotFound", "The tenant provided can't be found"));
                string serializer = JsonSerializer.Serialize(result);
                await context.Response.WriteAsync(serializer);
                return;
            }
        }
        await next.Invoke(context);
    }

    private string? GetTenantFromHost(string host)
    {
        string appHost = configuration.GetValue<string>("AppHost");
        if (appHost == null || !host.EndsWith(appHost, StringComparison.OrdinalIgnoreCase))
        {
            return null;
        }

        if (host.Length <= 0)
        {
            return null;
        }

        string prefix = host.Substring(0, host.Length -  appHost.Length);
        string tenant = prefix.TrimEnd('.');
        return tenant;

    }
}
