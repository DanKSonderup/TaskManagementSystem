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
        public Task AddAsync(ProjectTask task);
        public Task<ProjectTask> GetByIdAsync(Guid taskId);
        public Task<ProjectTask> UpdateAsync(ProjectTask task);
        public Task<IEnumerable<ProjectTask>> GetAllAsync();
        public Task DeleteAsync(ProjectTask task);
    }
}
