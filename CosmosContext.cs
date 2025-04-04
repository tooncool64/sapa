﻿using Microsoft.EntityFrameworkCore;

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
            modelBuilder.HasDefaultContainer("Appeals");
            
            // Configure AppealForm
            modelBuilder.Entity<AppealForm>()
                .ToContainer("Appeals")
                .HasPartitionKey(e => e.Id)
                .HasKey(e => e.Id);
    
            // Configure Semester1Courses as owned entity collection
            modelBuilder.Entity<AppealForm>()
                .OwnsMany(e => e.Semester1Courses, semesterCourse =>
                {
                    semesterCourse.WithOwner().HasForeignKey("AppealFormId");
                    semesterCourse.Property(c => c.Id);
                    semesterCourse.HasKey("Id");
                });
            
            // Configure Semester2Courses as owned entity collection
            modelBuilder.Entity<AppealForm>()
                .OwnsMany(e => e.Semester2Courses, semesterCourse =>
                {
                    semesterCourse.WithOwner().HasForeignKey("AppealFormId");
                    semesterCourse.Property(c => c.Id);
                    semesterCourse.HasKey("Id");
                });
            
            // Configure Comments relationship - use OwnsMany for Cosmos DB
            modelBuilder.Entity<AppealForm>()
                .OwnsMany(e => e.Comments, comment =>
                {
                    comment.WithOwner().HasForeignKey("AppealFormId");
                    comment.Property(c => c.Id);
                    comment.HasKey("Id");
                });
            
            base.OnModelCreating(modelBuilder);
        }
    }
}