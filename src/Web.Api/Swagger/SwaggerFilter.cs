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
        string appHost = configuration.GetValue<string>("AppHost")!;
        bool hasTenantAttribute = context.ApiDescription.ActionDescriptor.EndpointMetadata
            .OfType<TenantRequiredAttribute>().Any();
        if (host.EndsWith(appHost, StringComparison.OrdinalIgnoreCase))
        {
            string prefix = host.Substring(startIndex: 0, host.Length -  appHost.Length);
            if (!string.IsNullOrEmpty(prefix))
            {
                if (hasTenantAttribute)
                {
                    return;
                }
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
