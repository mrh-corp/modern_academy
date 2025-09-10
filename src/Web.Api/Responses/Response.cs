using Facet.Extensions;
using SharedKernel;

namespace Web.Api.Responses;

public static class Response
{
    public static Result<TResponse> MapResponse<TSource, TResponse>(this Result<TSource> source) where TResponse: class, new()
    {
        return new Result<TResponse>(
            source.Value.ToFacet<TSource, TResponse>(),
            source.IsSuccess,
            source.Error
        );
    }
}
