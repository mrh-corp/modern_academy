using Application.Abstractions.Data;
using Application.Abstractions.Params;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using Web.Api.Infrastructure;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Web.Api.Middleware;

public class SubDomainTenantMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        IConfiguration configuration = context.RequestServices.GetRequiredService<IConfiguration>();
        ITenantContext tenantContext = context.RequestServices.GetRequiredService<ITenantContext>();
        IApplicationDbContext dbContext = context.RequestServices.GetRequiredService<IApplicationDbContext>();
        string host = context.Request.Host.Host;
        string? tenant = GetTenantFromHost(host, configuration);
        if (!string.IsNullOrEmpty(tenant))
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
            if (hasTenantRequired && (tenant is null || tenantContext.Academy is null))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                var result = Result.Failure(Error.Problem("Tenant.NotFound", "The tenant provided can't be found"));
                string serializer = JsonSerializer.Serialize(result);
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(serializer);
                return;
            }
        }
        await next.Invoke(context);
    }

    private string? GetTenantFromHost(string host, IConfiguration configuration)
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
