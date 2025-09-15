using System.Collections;
using System.Reflection;
using Application.Abstractions.Params;
using Application.Storage;
using SharedKernel;

namespace Infrastructure.Helpers;

public class ImageUrlHelper(
    IStorageRepository storageRepository,
    ITenantContext tenantContext)
{
    private string _bucketName => tenantContext.TenantName;
    public async Task UpdateImageUrls(object? obj)
    {
        if (obj == null)
        {
            return;
        }

        if (obj is IEnumerable enumerable && !(obj is string))
        {
            foreach (object item in enumerable)
            {
                await UpdateImageUrls(item);
            }
            return;
        }

        Type type = obj.GetType();
        if (IsSimpleType(type))
        {
            return;
        }

        IEnumerable<PropertyInfo> properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (PropertyInfo prop in properties)
        {
            object value = prop.GetValue(obj);
            if (prop.Name.Contains("AttachmentUrl") && value is string str)
            {
                string url = await Transform(str, _bucketName);
                prop.SetValue(obj, url);
            }
            else
            {
                if (prop.PropertyType != typeof(string))
                {
                    await UpdateImageUrls(value);
                }
                
            }
        }
    }

    private async Task<string> Transform(string transform, string bucketName)
    {
        string url = await storageRepository.GetFileUrlAsync(transform, bucketName);
        return url;
    }
    private static bool IsSimpleType(Type type)
    {
        return type.IsPrimitive || type.IsEnum ||
               type == typeof(string) ||
               type == typeof(decimal) ||
               type == typeof(int) ||
               type == typeof(double) ||
               type == typeof(float) ||
               type == typeof(Guid) ||
               type == typeof(DateTime) ||
               type == typeof(DateTimeOffset) ||
               type == typeof(Guid) ||
               type == typeof(TimeSpan);
    }
}
