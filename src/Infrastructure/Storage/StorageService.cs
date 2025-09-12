using System.Globalization;
using System.Net;
using Application.Abstractions.Cache;
using Application.Storage;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Minio;
using Minio.DataModel.Args;
using Minio.DataModel.Response;
using SharedKernel;
using OneOf;
using StackExchange.Redis;

namespace Infrastructure.Storage;

public class StorageService(
    IMinioClient s3Client,
    IConfiguration configuration,
    ICacheManager cache,
    IHttpClientFactory httpClientFactory) : IStorageRepository
{
    private string BucketName => configuration.GetSection("S3")["Bucket"];
    private HttpClient? _httpClient;

    private HttpClient HttpClient
    {
        get
        {
            _httpClient ??= httpClientFactory.CreateClient();
            return _httpClient;
        }
    }

    public async Task<string> UploadFileAsync(string filename, Stream fileStream)
    {
        try
        {
            // Ensure bucket exists
            bool found = await s3Client.BucketExistsAsync(new BucketExistsArgs().WithBucket(BucketName));
            if (!found)
            {
                await s3Client.MakeBucketAsync(new MakeBucketArgs().WithBucket(BucketName));
            }

            PutObjectArgs putObjectArgs = new PutObjectArgs()
                .WithBucket(BucketName)
                .WithObject(filename)
                .WithContentType(GetContentType(filename))
                .WithStreamData(fileStream)
                .WithObjectSize(fileStream.Length);

            PutObjectResponse response = await s3Client.PutObjectAsync(putObjectArgs);

            if (response.ResponseStatusCode != HttpStatusCode.OK)
            {
                throw new StorageException($"Error uploading file {filename}");
            }

            return filename;
        }
        catch (Exception ex)
        {
            throw new StorageException($"Unhandled error on uploading file {ex.Message}");
        }
    }

    public Task<Stream> DownloadFileAsync(string filename)
    {
        throw new NotImplementedException();
    }

    public async Task<string> GetFileUrlAsync(string filename, int expireInMinutes = 5)
    {
        string cacheKey = $"{filename}-{BucketName}";
        string cacheUrl = await cache.RedisDb.StringGetAsync(cacheKey);
        if (cacheUrl == null)
        {
            string contentType = GetContentType(filename);
            var headers = new Dictionary<string, string> { { "Content-Type", contentType } };
            string presignedUrl = await s3Client.PresignedGetObjectAsync(
                new PresignedGetObjectArgs()
                    .WithBucket(BucketName)
                    .WithObject(filename)
                    .WithExpiry(expireInMinutes * 60)
                    .WithHeaders(headers)
            );
            TimeSpan ttl = DateTime.UtcNow.AddMinutes(expireInMinutes) - DateTime.UtcNow;
            await cache.RedisDb.StringSetAsync(cacheKey, presignedUrl, ttl);
            cacheUrl = presignedUrl;
        }
        
        return cacheUrl;
    }

    public async Task<FileInformation> GetFile(string filename)
    {
        string cacheKey = $"{BucketName}:{filename}";
        
        byte[]? cachedFile = await cache.RedisDb.StringGetAsync(cacheKey);
        if (cachedFile is null)
        {
            string minioUrl = await GetFileUrlAsync(filename);
            using HttpResponseMessage response = await HttpClient.GetAsync(minioUrl);
            if (!response.IsSuccessStatusCode)
            {
                throw new StorageException($"Error getting the file {filename}");
            }

            cachedFile = await response.Content.ReadAsByteArrayAsync();
            
            TimeSpan ttl = DateTime.UtcNow.AddHours(72) - DateTime.UtcNow;
            await cache.RedisDb.StringSetAsync(cacheKey, cachedFile, ttl);
        }
        return new FileInformation(cachedFile, GetContentType(filename));
    }

    public async Task RemoveFileAsync(string filename)
    {
        bool found = await s3Client.BucketExistsAsync(new BucketExistsArgs().WithBucket(BucketName));
        if (!found)
        {
            await s3Client.MakeBucketAsync(new MakeBucketArgs().WithBucket(BucketName));
        }
        await s3Client.RemoveObjectAsync(new RemoveObjectArgs()
            .WithBucket(BucketName)
            .WithObject(filename));
    }

    public async Task<bool> RemoveFileCacheKey(string filename)
    {
        string key = $"{BucketName}:{filename}";
        return await cache.RedisDb.KeyDeleteAsync(key);
    }

    private string GetContentType(string filename)
    {
        var provider = new FileExtensionContentTypeProvider();
        if (!provider.TryGetContentType(filename, out string contentType))
        {
            contentType = "application/octet-stream";
        }

        return contentType;
    }
}
