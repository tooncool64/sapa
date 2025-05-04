using Newtonsoft.Json;

namespace BlazorApp
{
    public class AppealComment
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        public string Text { get; set; }
        public string Status { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string AdminId { get; set; }
        public string AdminName { get; set; }
    }
}