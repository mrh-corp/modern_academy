using Application.Abstractions.Service;
using OneOf;
using SharedKernel;

namespace Application.Storage;

public interface IStorageRepository : IService
{
    Task<string> UploadFileAsync(string objectKey, string filename, Stream fileStream, string bucketName);
    Task<Stream> DownloadFileAsync(string filename);
    Task<string> GetFileUrlAsync(string filename, string bucketName, int expireInMinutes = 5);
    Task<FileInformation> GetFile(string filename, string bucketName);
    Task RemoveFileAsync(string filename, string bucketName);
    Task<bool> RemoveFileCacheKey(string filename, string bucketName);

}
