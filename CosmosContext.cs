using Microsoft.EntityFrameworkCore;

namespace BlazorApp
{
    public class CosmosContext : DbContext
    {
        public CosmosContext(DbContextOptions<CosmosContext> options) : base(options) { }
        
        public DbSet<AppealForm> Appeals { get; set; }
    }
}