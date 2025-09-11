using System.Net;
using Amazon.S3;
using Amazon.S3.Model;
using Application.Storage;
using Microsoft.Extensions.Configuration;
using SharedKernel;
using OneOf;

namespace Infrastructure.Storage;

public class StorageService(IAmazonS3 s3Client, IConfiguration configuration) : IStorateRepository
{
    private readonly string _bucketName = configuration["Storage:Bucket"] ?? "storage";

    public async Task<OneOf<Error, string>> UploadFileAsync(string filename, Stream fileStream)
    {
        await s3Client.EnsureBucketExistsAsync(_bucketName);
        string objectKey = filename.Trim('/');
        var putRequest = new PutObjectRequest
        {
            BucketName = _bucketName,
            Key = objectKey,
            InputStream = fileStream
        };
        PutObjectResponse response = await s3Client.PutObjectAsync(putRequest);
        if (response.HttpStatusCode != HttpStatusCode.OK)
        {
            return Error.Problem($"S3.{response.HttpStatusCode}", $"Error uploading file {filename}");
        }

        return objectKey;
    }

    public Task<Stream> DownloadFileAsync(string filename)
    {
        throw new NotImplementedException();
    }

    public Task<string> GetFileUrlAsync(string filename, int expireInHours = 72)
    {
        string key = filename.Trim('/');
        var request = new GetPreSignedUrlRequest
        {
            BucketName = _bucketName,
            Key = key,
            Expires = DateTime.UtcNow.AddHours(expireInHours)
        };
        
        return s3Client.GetPreSignedURLAsync(request);
    }
}
