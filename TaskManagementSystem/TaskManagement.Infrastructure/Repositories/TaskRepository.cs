using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Interfaces;
using TaskManagement.Domain.Entities;
using TaskManagement.Infrastructure.Data;

namespace TaskManagement.Infrastructure.Repositories
{
    public class TaskRepository: ITaskRepository
    {
        private readonly AppDbContext _context;

        public TaskRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ProjectTask task)
        {
            await _context.ProjectTasks.AddAsync(task);
            await _context.SaveChangesAsync();
        }

        public async Task<ProjectTask> GetByIdAsync(Guid taskId)
        {
            var result = await _context.ProjectTasks.FindAsync(taskId);
            if (result == null)
            {
                throw new Exception("Task not found"); 
            }
            return result;
        }

        public async Task<IEnumerable<ProjectTask>> GetAllAsync()
        {
            return await _context.ProjectTasks.ToListAsync();
        }

        public async Task DeleteAsync(ProjectTask task)
        {
            var foundTask = await _context.ProjectTasks.FindAsync(task.Id);
            if (foundTask != null)
            {
                _context.ProjectTasks.Remove(foundTask);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<ProjectTask> UpdateAsync(ProjectTask task)
        {
            _context.ProjectTasks.Update(task);
            await _context.SaveChangesAsync();
            return task;
        }
    }
}
