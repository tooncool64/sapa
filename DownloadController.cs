using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models; // Required for BlobProperties, PublicAccessType etc.
using Microsoft.AspNetCore.Mvc;
using Azure; // Required for RequestFailedException

namespace BlazorApp
{
    [Route("api/[controller]")] // Use controller name convention
    [ApiController]
    // [Authorize(Roles = "Admin")] // *** IMPORTANT: Secure this endpoint ***
    public class DownloadController : ControllerBase
    {
        private readonly string _connectionString;
        private readonly ILogger<DownloadController> _logger;
        private const string ContainerName = "blobfilestorage"; // Ensure this matches UploadController

        public DownloadController(IConfiguration configuration, ILogger<DownloadController> logger)
        {
            _connectionString = Environment.GetEnvironmentVariable("AzureAd__StorageKey");

            _logger = logger;
        }

        // GET api/download/{fileId}
        [HttpGet("{fileId}")]
        public async Task<IActionResult> GetFile(string fileId)
        {
            if (string.IsNullOrWhiteSpace(fileId))
            {
                _logger.LogWarning("Download attempt with empty fileId.");
                return BadRequest("File identifier is required.");
            }

            // Basic sanitization (optional, but good practice)
            // Prevent directory traversal - though GUIDs are generally safe
            if (fileId.Contains("/") || fileId.Contains("\\") || fileId == ".." || fileId == ".")
            {
                 _logger.LogWarning("Potential path traversal attempt in fileId: {FileId}", fileId);
                 return BadRequest("Invalid file identifier.");
            }

            _logger.LogInformation("Attempting to download file with ID: {FileId}", fileId);

            try
            {
                var blobServiceClient = new BlobServiceClient(_connectionString);
                var containerClient = blobServiceClient.GetBlobContainerClient(ContainerName);

                // Optional: Check if container exists (helps diagnose config issues)
                if (!await containerClient.ExistsAsync())
                {
                     _logger.LogError("Blob container '{ContainerName}' not found.", ContainerName);
                     return NotFound($"Storage container not found."); // Or a generic 500
                }

                var blobClient = containerClient.GetBlobClient(fileId); // Use the unique FileId

                if (!await blobClient.ExistsAsync())
                {
                    _logger.LogWarning("File not found in blob storage. Container: {ContainerName}, FileId: {FileId}", ContainerName, fileId);
                    return NotFound($"File not found.");
                }

                // Get blob properties to determine Content-Type and Original Filename (from metadata)
                Response<BlobProperties> properties = await blobClient.GetPropertiesAsync();
                var contentType = properties.Value.ContentType ?? "application/octet-stream"; // Default if not set
                string downloadFileName = fileId; // Default to FileId

                // Try to get the original filename from metadata (assuming it was set during upload)
                if (properties.Value.Metadata.TryGetValue("OriginalFileName", out var originalFileName) && !string.IsNullOrWhiteSpace(originalFileName))
                {
                    // Sanitize originalFileName before using in header
                    downloadFileName = Path.GetFileName(originalFileName); // Basic sanitization
                    _logger.LogInformation("Retrieved original filename '{OriginalFileName}' from metadata for FileId '{FileId}'", downloadFileName, fileId);
                }
                else
                {
                     _logger.LogWarning("OriginalFileName metadata not found or empty for FileId '{FileId}'. Using FileId as download name.", fileId);
                }


                // Get the blob stream
                var stream = await blobClient.OpenReadAsync();

                // Use FileStreamResult for efficient streaming
                return File(stream, contentType, downloadFileName);

            }
            catch (RequestFailedException ex) when (ex.Status == 404)
            {
                 _logger.LogWarning(ex, "Blob not found exception for FileId: {FileId}", fileId);
                 return NotFound("File not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error downloading file with ID: {FileId}", fileId);
                // Return a generic error to the client
                return StatusCode(500, "An error occurred while retrieving the file.");
            }
        }
    }
}