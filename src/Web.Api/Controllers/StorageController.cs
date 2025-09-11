using Application.Storage;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Controllers;

[Route("api/file")]
public class StorageController(IStorateRepository storateRepository) : Controller
{
    [HttpGet("{*filename}")]
    public async Task<ActionResult<string>> GetFile(string filename)
    {
        string fileUrl = await storateRepository.GetFileUrlAsync(filename);
        return Ok(fileUrl);
    }
}
