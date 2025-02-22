using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp;

[Route("api/upload")]
[ApiController]
public class UploadController : ControllerBase
{
    private readonly string? _connectionString = Environment.GetEnvironmentVariable("AzureAd__StorageKey");
    private const string ContainerName = "blobfilestorage";

    [HttpPost]
    public async Task<IActionResult> UploadFile([FromForm] IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("No file uploaded.");
        }

        try
        {
            var blobServiceClient = new BlobServiceClient(_connectionString);
            var containerClient = blobServiceClient.GetBlobContainerClient(ContainerName);
            await containerClient.CreateIfNotExistsAsync();

            var blobClient = containerClient.GetBlobClient(file.FileName);
            await using (var stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, true);
            }

            return Ok(new { FileUrl = blobClient.Uri.ToString() });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }
}