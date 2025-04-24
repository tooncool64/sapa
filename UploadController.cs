using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp
{
    [Route("api/upload")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        // --- Use Dependency Injection for Configuration and Logging ---
        private readonly string _connectionString;
        private readonly ILogger<UploadController> _logger;
        private const string ContainerName = "blobfilestorage"; // Consider moving to configuration if needed

        public UploadController(IConfiguration configuration, ILogger<UploadController> logger)
        {
            _connectionString = Environment.GetEnvironmentVariable("AzureAd__StorageKey");

            _logger = logger;
        }

        [HttpPost]
        // Consider adding request size limit attribute if needed: [RequestSizeLimit(10 * 1024 * 1024)] // 10 MB
        public async Task<IActionResult> UploadFile([FromForm] IFormFile file)
        {
            if (string.IsNullOrEmpty(_connectionString))
            {
                 _logger.LogError("Azure Storage Connection String is not configured correctly.");
                 return StatusCode(500, "Server configuration error."); // Don't expose specifics
            }

            if (file == null || file.Length == 0)
            {
                _logger.LogWarning("Upload attempt with no file.");
                return BadRequest("No file uploaded or file is empty.");
            }

            // Optional: Add more validation (e.g., file size, allowed content types)
            // if (!IsValidContentType(file.ContentType)) return BadRequest("Invalid file type.");
            // if (file.Length > MaxFileSize) return BadRequest("File size exceeds limit.");

            try
            {
                var blobServiceClient = new BlobServiceClient(_connectionString);
                var containerClient = blobServiceClient.GetBlobContainerClient(ContainerName);

                // Ensure container exists (fine for most cases, consider creating during deployment for high perf)
                await containerClient.CreateIfNotExistsAsync(Azure.Storage.Blobs.Models.PublicAccessType.None); // Ensure private access

                // --- Generate a UNIQUE filename to prevent collisions ---
                var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                _logger.LogInformation("Generated unique filename: {UniqueFileName} for original file: {OriginalFileName}", uniqueFileName, file.FileName);

                var blobClient = containerClient.GetBlobClient(uniqueFileName);

                _logger.LogInformation("Attempting to upload blob {BlobName} to container {ContainerName}", blobClient.Name, containerClient.Name);

                await using (var stream = file.OpenReadStream())
                {
                    // UploadAsync without overwrite flag (since name is unique)
                    await blobClient.UploadAsync(stream, CancellationToken.None); // Pass CancellationToken if available
                }

                _logger.LogInformation("Successfully uploaded blob {BlobName}. URI: {BlobUri}", blobClient.Name, blobClient.Uri);

                // --- Return the identifier EXPECTED by the Blazor component ---
                // Your Blazor code expects 'FileId'. Let's return the unique name.
                return Ok(new { FileId = uniqueFileName });

                // If you decided to use the URL as the identifier in Blazor, you would return this:
                // return Ok(new { FileUrl = blobClient.Uri.ToString() });

            }
            catch (Exception ex)
            {
                // Log the full exception for diagnostics
                _logger.LogError(ex, "Error uploading file {FileName}. Original Name: {OriginalFileName}", file.FileName, file.FileName); // Use structured logging

                // Return a generic error message to the client
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}