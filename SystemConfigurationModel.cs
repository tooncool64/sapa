using System;
using System.Text.Json.Serialization;

namespace BlazorApp
{
    public class AppSettings
    {
        [JsonPropertyName("appealClosingDate")]
        public string AppealClosingDate { get; set; }
        
        [JsonPropertyName("lastModified")]
        public DateTime LastModified { get; set; }
        
        [JsonPropertyName("modifiedBy")]
        public string ModifiedBy { get; set; }
    }
}