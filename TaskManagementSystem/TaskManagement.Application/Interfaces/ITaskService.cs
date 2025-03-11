using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Enums;

namespace TaskManagement.Application.Interfaces
{
    public interface ITaskService
    {
        Task<Guid> CreateTaskAsync(string title, string description);
        Task<IEnumerable<ProjectTask>> GetAllTasksAsync();
        Task<ProjectTask?> GetTaskByIdAsync(Guid taskId);
        Task<bool> UpdateTaskAsync(Guid taskId, string title, string description, TaskState status);
        Task<bool> DeleteTaskAsync(Guid taskId);
    }
}
