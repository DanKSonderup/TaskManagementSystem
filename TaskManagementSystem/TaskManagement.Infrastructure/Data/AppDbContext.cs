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
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<ProjectTask>()
           .HasOne(p => p.Department) 
           .WithMany(d => d.ProjectTasks)
           .HasForeignKey(p => p.DepartmentId)
           .OnDelete(DeleteBehavior.Restrict);

            var itDepartmentId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var salesDepartmentId = Guid.Parse("33333333-3333-3333-3333-333333333333");

            modelBuilder.Entity<Department>().HasData(
                new Department { Id = itDepartmentId, Name = "IT" },
                new Department { Id = salesDepartmentId, Name = "Sales" }
            );

            modelBuilder.Entity<ProjectTask>().HasData(
                new ProjectTask
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Title = "Create Database",
                    Description = "Set up initial database",
                    DepartmentId = itDepartmentId
                },
                new ProjectTask
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    Title = "Implement CRUD",
                    Description = "Implement CRUD operations",
                    DepartmentId = itDepartmentId
                },
                new ProjectTask
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    Title = "Make Powerpoint slide",
                    Description = "Make Quarter PP",
                    DepartmentId = salesDepartmentId
                }
            );
        }
    }
}
