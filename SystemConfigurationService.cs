using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;

namespace BlazorApp
{
    public interface IAppealClosingDateService
    {
        Task<DateTime?> GetClosingDateAsync();
        Task<bool> IsAppealPeriodOpenAsync();
        Task SetClosingDateAsync(DateTime closingDate, string modifiedBy);
        Task RemoveClosingDateAsync(string modifiedBy);
    }

    public class JsonAppealClosingDateService : IAppealClosingDateService
    {
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger<JsonAppealClosingDateService> _logger;
        private readonly string _settingsFilePath;
        private static readonly object _fileLock = new object();

        public JsonAppealClosingDateService(
            IWebHostEnvironment environment,
            ILogger<JsonAppealClosingDateService> logger)
        {
            _environment = environment;
            _logger = logger;
            _settingsFilePath = Path.Combine(_environment.ContentRootPath, "appsettings.json");
        }

        private async Task<AppSettings> ReadSettingsAsync()
        {
            if (!File.Exists(_settingsFilePath))
            {
                return new AppSettings();
            }

            try
            {
                var json = await File.ReadAllTextAsync(_settingsFilePath);
                return JsonSerializer.Deserialize<AppSettings>(json) ?? new AppSettings();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error reading settings file");
                return new AppSettings();
            }
        }

        private async Task SaveSettingsAsync(AppSettings settings)
        {
            var json = JsonSerializer.Serialize(settings, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            // Use a lock to prevent concurrent file access issues
            lock (_fileLock)
            {
                File.WriteAllText(_settingsFilePath, json);
            }
        }

        public async Task<DateTime?> GetClosingDateAsync()
        {
            var settings = await ReadSettingsAsync();
            
            if (string.IsNullOrEmpty(settings.AppealClosingDate))
                return null;

            if (DateTime.TryParse(settings.AppealClosingDate, out var date))
                return date;

            _logger.LogWarning("Invalid appeal closing date format in settings");
            return null;
        }

        public async Task<bool> IsAppealPeriodOpenAsync()
        {
            var closingDate = await GetClosingDateAsync();
            if (!closingDate.HasValue)
                return true; // If no closing date is set, appeals are allowed

            return DateTime.Now < closingDate.Value;
        }

        public async Task SetClosingDateAsync(DateTime closingDate, string modifiedBy)
        {
            var settings = await ReadSettingsAsync();
            
            settings.AppealClosingDate = closingDate.ToString("o");
            settings.LastModified = DateTime.UtcNow;
            settings.ModifiedBy = modifiedBy;
            
            await SaveSettingsAsync(settings);
            
            _logger.LogInformation($"Appeal closing date set to {closingDate} by {modifiedBy}");
        }
        
        public async Task RemoveClosingDateAsync(string modifiedBy)
        {
            var settings = await ReadSettingsAsync();
            
            settings.AppealClosingDate = null;
            settings.LastModified = DateTime.UtcNow;
            settings.ModifiedBy = modifiedBy;
            
            await SaveSettingsAsync(settings);
            
            _logger.LogInformation($"Appeal closing date removed by {modifiedBy}");
        }
    }
}