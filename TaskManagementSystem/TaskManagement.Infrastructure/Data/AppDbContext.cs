using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Infrastructure.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<ProjectTask> ProjectTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProjectTask>().HasData(
                new ProjectTask
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Title = "Create Database",
                    Description = "Set up initial database",
                },
                new ProjectTask
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    Title = "Implement CRUD",
                    Description = "Implement CRUD operations",
                },
                new ProjectTask
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    Title = "Test API",
                    Description = "Test all API endpoints",
                }
            );
        }
    }
}
