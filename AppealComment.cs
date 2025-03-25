namespace BlazorApp
{
    public class AppealComment
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Text { get; set; }
        public string Status { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string AdminId { get; set; }
        public string AdminName { get; set; }
        
        // Note: We don't need AppealId or navigation property in a Cosmos DB owned entity
        // as it's automatically part of the parent document
    }
}