using Application.Abstractions.Params;
using Application.Storage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Helpers;

public class ImageUrlFilter : IAsyncResultFilter
{
    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        if (context.Result is ObjectResult objectResult)
        {
            IStorageRepository storageRepository =
                context.HttpContext.RequestServices.GetRequiredService<IStorageRepository>();
            ITenantContext tenantContext =
                context.HttpContext.RequestServices.GetRequiredService<ITenantContext>();
            var helper = new ImageUrlHelper(storageRepository, tenantContext);
            await helper.UpdateImageUrls(objectResult.Value);
        }
        await next();
    }
}
