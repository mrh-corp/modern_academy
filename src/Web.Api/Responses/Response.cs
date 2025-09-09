using System.Reflection;
using SharedKernel;

namespace Web.Api.Responses;

public static class Response
{
    public static Result<TResponse> ToResponse<TResponse, TSource>(this Result<TSource> source) where TResponse : new()
    {
        var response = new TResponse();
        TSource result = source.Value;
        PropertyInfo[] sourceProps = typeof(TSource).GetProperties();
        PropertyInfo[] targetProps = typeof(TSource).GetProperties();

        foreach (PropertyInfo targetProp in targetProps)
        {
            PropertyInfo? sourceProp = sourceProps.FirstOrDefault(x => x.Name == targetProp.Name);
            if (sourceProp is not null && targetProp.CanWrite)
            {
                object? value = sourceProp.GetValue(result, null);
                targetProp.SetValue(response, value);
            }
        }

        return new Result<TResponse>(
            response,
            source.IsSuccess,
            source.Error);
    }
}
