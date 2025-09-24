using System.Text.Json;
using Application.Abstractions.Params;
using Microsoft.Extensions.Primitives;
using SharedKernel;
using Web.Api.Infrastructure;

namespace Web.Api.Middleware;

public class ActiveParamsMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        IHeaderDictionary headers =  context.Request.Headers;
        bool requiredActiveParams = context.GetEndpoint()?.Metadata
            .GetMetadata<ActiveParamsRequiredAttribute>() != null;
        if (!requiredActiveParams)
        {
            await next.Invoke(context);
            return;
        }
        if (!headers.TryGetValue("School-Year", out StringValues schoolYear))
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            var result = Result.Failure(Error.Problem("ActiveParams.NotFound", "School year id should be provided"));
            string serializer = JsonSerializer.Serialize(result);
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(serializer);
            return;
        }

        IActiveParamsContext service = context.RequestServices.GetRequiredService<IActiveParamsContext>();
        service.SchoolYearId = Guid.Parse(schoolYear.ToString());
        if(service.ActiveSchoolYear is null)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            var result = Result.Failure(Error.Problem("ActiveParams.NotFound", "School year not found"));
            string serializer = JsonSerializer.Serialize(result);
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(serializer);
            return;
        }

        await next.Invoke(context);
    }
}
