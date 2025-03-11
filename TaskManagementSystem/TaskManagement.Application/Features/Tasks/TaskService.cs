using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Interfaces;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Enums;

namespace TaskManagement.Application.Features.Tasks
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }
        public async Task<Guid> CreateTaskAsync(string title, string description)
        {
            ProjectTask task = new ProjectTask(title, description);
            await _taskRepository.AddAsync(task);
            return task.Id;
        }

        public Task<bool> DeleteTaskAsync(Guid taskId)
        {
            ProjectTask projectTask = _taskRepository.GetByIdAsync(taskId).Result;
            if (projectTask == null)
            {
                return Task.FromResult(false);
            }
            _taskRepository.DeleteAsync(projectTask);
            return Task.FromResult(true);
        }

        // Vi bruger IEnumerable i stedet for list sådan at kalderen af kaldet selv kan bestemme den liste
        // type de vil bruge
        // Dette overholder Liskovs Substitution Principle
        public async Task<IEnumerable<ProjectTask>> GetAllTasksAsync()
        {
            var projectTasks = await _taskRepository.GetAllAsync();
            return projectTasks ?? Enumerable.Empty<ProjectTask>();
        }

        public async Task<ProjectTask?> GetTaskByIdAsync(Guid taskId)
        {
            return await _taskRepository.GetByIdAsync(taskId);
        }

        public async Task<bool> UpdateTaskAsync(Guid taskId, string title, string description, TaskState status)
        {
            var projectTask = await _taskRepository.GetByIdAsync(taskId);
            if (projectTask == null)
            {
                return false;
            }
            projectTask.Update(title, description, status);

            await _taskRepository.UpdateAsync(projectTask);
            return true;
        }
    }
}
