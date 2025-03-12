using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System;
using TaskManagement.Application.Features.Departments;
using TaskManagement.Application.Features.Departments.DTO;
using TaskManagement.Application.Features.Departments.Validators;
using TaskManagement.Application.Features.Tasks;
using TaskManagement.Application.Features.Tasks.DTO;
using TaskManagement.Application.Features.Tasks.Validators;
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

            builder.Services.AddControllersWithViews();
            builder.Services.AddControllers();


            builder.Services.AddHttpClient("API", client =>
            {
                client.BaseAddress = new Uri("https://localhost:7052/");
            });

            // Register Application Layer Services
            builder.Services.AddScoped<ITaskService, TaskService>();
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();

            // Application Layer Validators
            builder.Services.AddTransient(typeof(IValidator<>), typeof(BaseProjectTaskDtoValidator<>));
            builder.Services.AddTransient<IValidator<DepartmentDto>, DepartmentValidator>();

            // Register Infrastructure Layer Repositories
            builder.Services.AddScoped<ITaskRepository, TaskRepository>();
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();


            // Configure EF Core with SQL Server
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")).UseLazyLoadingProxies());

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
            app.MapControllerRoute(name: "default", pattern: "{controller=Tasks}/{action=Index}/{id?}");

            app.MapGet("/", async context =>
            {
                context.Response.Redirect("/Tasks");
            });

            app.Run();
        }
    }
}
