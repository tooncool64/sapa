using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models; // Required for BlobUploadOptions, PublicAccessType etc.
using Microsoft.AspNetCore.Mvc;
using Azure; // For Task

namespace BlazorApp
{
    [Route("api/upload")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        // --- Injected Services & Configuration ---
        private readonly string _connectionString;
        private readonly ILogger<UploadController> _logger;
        private const string ContainerName = "blobfilestorage"; // Ensure this container exists or is created

        // --- Constructor for Dependency Injection ---
        public UploadController(IConfiguration configuration, ILogger<UploadController> logger)
        {
            _connectionString = Environment.GetEnvironmentVariable("AzureAd__StorageKey");

            _logger = logger;
        }

        // --- POST Endpoint for File Upload ---
        [HttpPost]
        // Optional: Add attribute to limit request size if needed
        // [RequestSizeLimit(10 * 1024 * 1024)] // Example: 10 MB limit matching Blazor code
        public async Task<IActionResult> UploadFile([FromForm] IFormFile file, CancellationToken cancellationToken = default)
        {
            // --- Input Validation ---
            if (string.IsNullOrEmpty(_connectionString)) // Should have been caught by constructor, but double-check
            {
                 _logger.LogError("UploadFile endpoint called but Azure Storage Connection String is missing.");
                 return StatusCode(StatusCodes.Status500InternalServerError, "Server configuration error related to storage.");
            }

            if (file == null || file.Length == 0)
            {
                _logger.LogWarning("UploadFile attempt with no file or empty file.");
                return BadRequest("No file uploaded or file is empty.");
            }

            // Optional: Add more specific validation here if needed (e.g., allowed extensions, content type sniffing)
            // var allowedExtensions = new[] { ".pdf", ".docx" };
            // var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
            // if (!allowedExtensions.Contains(fileExtension)) { return BadRequest("Invalid file type."); }

            _logger.LogInformation("UploadFile request received for file: {OriginalFileName}, Size: {FileSize} bytes", file.FileName, file.Length);

            try
            {
                // --- Connect to Azure Blob Storage ---
                var blobServiceClient = new BlobServiceClient(_connectionString);
                var containerClient = blobServiceClient.GetBlobContainerClient(ContainerName);

                // Ensure container exists (use 'None' for private access)
                await containerClient.CreateIfNotExistsAsync(PublicAccessType.None, cancellationToken: cancellationToken);
                _logger.LogInformation("Ensured blob container '{ContainerName}' exists.", ContainerName);

                // --- Generate Unique Filename & Prepare Metadata ---
                var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                _logger.LogInformation("Generated unique filename: {UniqueFileName} for original file: {OriginalFileName}", uniqueFileName, file.FileName);

                var metadata = new Dictionary<string, string>
                {
                    // Store the original filename in metadata for retrieval during download
                    { "OriginalFileName", file.FileName }
                    // You could add other relevant metadata here, e.g., UploadTimestamp, UserId
                    // { "UploadTimestampUtc", DateTime.UtcNow.ToString("o") }
                };

                var blobClient = containerClient.GetBlobClient(uniqueFileName);

                // --- Perform Upload ---
                _logger.LogInformation("Attempting to upload blob {BlobName} to container {ContainerName} with metadata.", blobClient.Name, containerClient.Name);

                // Use 'await using' for the stream to ensure disposal
                await using (var stream = file.OpenReadStream())
                {
                    var uploadOptions = new BlobUploadOptions
                    {
                        Metadata = metadata,
                        // Optional: Set ContentType if known and reliable, otherwise DownloadController can check properties
                        // HttpHeaders = new BlobHttpHeaders { ContentType = file.ContentType }
                    };

                    // Upload the stream content with options (includes metadata)
                    await blobClient.UploadAsync(stream, uploadOptions, cancellationToken);
                }

                _logger.LogInformation("Successfully uploaded blob {BlobName}. URI: {BlobUri}", blobClient.Name, blobClient.Uri);

                // --- Return Success Response ---
                // The response body MUST contain the unique filename under the key 'FileId'
                // as expected by the Blazor component's UploadResponse class.
                return Ok(new { FileId = uniqueFileName });

            }
            catch (RequestFailedException azureEx)
            {
                 // Catch specific Azure exceptions if needed for tailored logging/handling
                 _logger.LogError(azureEx, "Azure Storage error during upload of file {OriginalFileName}. ErrorCode: {ErrorCode}, Status: {Status}", file.FileName, azureEx.ErrorCode, azureEx.Status);
                 return StatusCode(StatusCodes.Status500InternalServerError, $"A storage error occurred while processing your request. ErrorCode: {azureEx.ErrorCode}");
            }
            catch (Exception ex)
            {
                // Catch general exceptions
                _logger.LogError(ex, "Unexpected error during upload of file {OriginalFileName}", file.FileName);

                // Return a generic error message to the client
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred while processing your request.");
            }
        }
    }
}