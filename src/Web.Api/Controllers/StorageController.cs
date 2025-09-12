using Application.Storage;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;

namespace Web.Api.Controllers;

[Route("media")]
public class StorageController(IStorageRepository storageRepository) : Controller
{
    [HttpGet("{filename}")]
    public async Task<ActionResult> GetFile(string filename)
    {
        try
        {
            FileInformation result = await storageRepository.GetFile(filename);
            return File(result.Content, result.ContentType, enableRangeProcessing: true);
        }
        catch (Exception ex)
        {
            return NotFound(
                Result.Failure(
                    Error.NotFound("File.NotFound", ex.Message)));
        }
    }
}
