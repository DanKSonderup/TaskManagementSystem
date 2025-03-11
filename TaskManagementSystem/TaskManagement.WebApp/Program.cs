using Microsoft.EntityFrameworkCore;
using System;
using TaskManagement.Application.Features.Tasks;
using TaskManagement.Application.Interfaces;
using TaskManagement.Infrastructure.Data;
using TaskManagement.Infrastructure.Repositories;

namespace TaskManagement.WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container
            builder.Services.AddRazorPages();
            builder.Services.AddControllers();

            // Register Application Layer Services
            builder.Services.AddScoped<ITaskService, TaskService>();

            // Register Infrastructure Layer Repositories
            builder.Services.AddScoped<ITaskRepository, TaskRepository>();

            // Configure EF Core with SQL Server
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")));

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                dbContext.Database.Migrate(); // Applies any pending migrations
            }

            app.UseExceptionHandler("/Error");
            app.UseHsts(); // HTTP Strict Transport Security

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthorization();

            // Map Controllers and Razor Pages
            app.MapControllers();

            app.MapGet("/", async context =>
            {
                context.Response.Redirect("/Tasks");
            });
            app.MapRazorPages();

            app.Run();
        }
    }
}
