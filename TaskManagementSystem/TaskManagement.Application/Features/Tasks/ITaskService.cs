using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Features.Tasks.DTO;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Enums;

namespace TaskManagement.Application.Features.Tasks
{
    public interface ITaskService
    {
        Task<ProjectTask?> CreateTaskAsync(CreateProjectTaskDto dto);
        Task<IEnumerable<ProjectTask>> GetAllTasksAsync();
        Task<ProjectTask?> GetTaskByIdAsync(Guid taskId);
        Task<bool> UpdateTaskAsync(Guid taskId, string title, string description, TaskState status);
        Task<bool> DeleteTaskAsync(Guid taskId);
    }
}
