using Application.Abstractions.Params;
using Microsoft.Extensions.Primitives;

namespace Web.Api.Middleware;

public class ActiveParamsMiddleware(RequestDelegate next)
{
    public Task InvokeAsync(HttpContext context)
    {
        IHeaderDictionary headers =  context.Request.Headers;
        if (!headers.TryGetValue("School-Year", out StringValues schoolYear))
        {
            return next.Invoke(context);
        }

        IActiveParamsContext service = context.RequestServices.GetRequiredService<IActiveParamsContext>();
        service.SchoolYearId = Guid.Parse(schoolYear.ToString());
        return next.Invoke(context);
    }
}
