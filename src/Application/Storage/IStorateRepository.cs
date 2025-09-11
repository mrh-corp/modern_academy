using Application.Abstractions.Service;
using OneOf;
using SharedKernel;

namespace Application.Storage;

public interface IStorateRepository : IService
{
    Task<OneOf<Error, string>> UploadFileAsync(string filename, Stream fileStream);
    Task<Stream> DownloadFileAsync(string filename);
    Task<string> GetFileUrlAsync(string filename, int expireInHours = 72);
}
