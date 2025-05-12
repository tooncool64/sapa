using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Services
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

            // Use CountAsync for better performance - queries the counts directly in the DB
            var totalCount = await context.Appeals.CountAsync();
            var pendingCount = await context.Appeals.CountAsync(a => a.Status == "Pending");
            var approvedCount = await context.Appeals.CountAsync(a => a.Status == "Approved");
            var rejectedCount = await context.Appeals.CountAsync(a => a.Status == "Rejected");

            return new Dictionary<string, int>
            {
                { "Total", totalCount },
                { "Approved", approvedCount },
                { "Rejected", rejectedCount },
                { "Pending", pendingCount },
            };
        }
    }
}