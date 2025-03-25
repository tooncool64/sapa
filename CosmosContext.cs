using Microsoft.EntityFrameworkCore;

namespace BlazorApp
{
    public class CosmosContext : DbContext
    {
        private readonly IConfiguration _configuration;
        
        public CosmosContext(DbContextOptions<CosmosContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }
        
        public DbSet<AppealForm> Appeals { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppealForm>()
                .ToContainer("Appeals")
                .HasPartitionKey(e => e.Id);
            
            base.OnModelCreating(modelBuilder);
        }
    }
}