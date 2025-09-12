using Application.Abstractions.Service;
using OneOf;
using SharedKernel;

namespace Application.Storage;

public interface IStorageRepository : IService
{
    Task<string> UploadFileAsync(string filename, Stream fileStream);
    Task<Stream> DownloadFileAsync(string filename);
    Task<string> GetFileUrlAsync(string filename, int expireInMinutes = 5);
    Task<FileInformation> GetFile(string filename);
    Task RemoveFileAsync(string filename);
    Task<bool> RemoveFileCacheKey(string filename);

}
