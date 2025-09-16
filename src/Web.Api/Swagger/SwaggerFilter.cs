using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Web.Api.Infrastructure;

namespace Web.Api.Swagger;

public class SwaggerFilter(IHttpContextAccessor httpContextAccessor, IConfiguration configuration) : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        HttpContext httpContext = httpContextAccessor.HttpContext;
        if (httpContext is null)
        {
            return;
        }

        string host = httpContext.Request.Host.Host;
        string appHost = configuration.GetValue<string>("AppHost");
        bool hasTenantAttribute = context.ApiDescription.ActionDescriptor.EndpointMetadata
            .OfType<TenantRequiredAttribute>().Any();
        if (appHost is not null && host.EndsWith(appHost, StringComparison.OrdinalIgnoreCase))
        {
            if (!hasTenantAttribute)
            {
                operation.Deprecated = true;
            }
        }
        else
        {
            if (hasTenantAttribute)
            {
                operation.Deprecated = true;
            }
        }
    }
}
