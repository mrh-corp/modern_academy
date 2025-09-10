namespace Web.Api.Middleware;

public class ActiveParamsMiddleware(RequestDelegate next)
{
    public Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Headers.TryGetValue("school-year", out var schoolYear))
        {
            
        }
    }
}
