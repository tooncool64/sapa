using Microsoft.EntityFrameworkCore;

namespace BlazorApp
{
    public class AppealCardService
    {
        private readonly IDbContextFactory<CosmosContext> _contextFactory;

        public AppealCardService(IDbContextFactory<CosmosContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<Dictionary<string, int>> GetAppealCountsAsync()
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            
            var appeals = await context.Appeals.ToListAsync();
            
            return new Dictionary<string, int>
            {
                { "Total", appeals.Count },
                { "Pending", appeals.Count(a => a.Status == "Pending") },
            };
        }
    }
}