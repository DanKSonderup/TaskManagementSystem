using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.Interfaces
{
    public interface ITaskRepository
    {
        Task AddAsync(ProjectTask task);
        Task<ProjectTask> GetByIdAsync(Guid taskId);
        Task<IEnumerable<ProjectTask>> GetAllAsync();
        Task DeleteAsync(ProjectTask task);
    }
}
