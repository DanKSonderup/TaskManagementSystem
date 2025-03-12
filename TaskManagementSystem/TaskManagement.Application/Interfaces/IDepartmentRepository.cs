using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Features.Departments.DTO;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.Interfaces
{
    public interface IDepartmentRepository
    {
        public Task AddAsync(Department department);
        public Task<List<DepartmentDto>> GetAllDepartmentsAsync();
        public Task<Department> GetByIdAsync(Guid departmentId);
        public Task<Department> UpdateAsync(Department department);
        public Task<IEnumerable<Department>> GetAllAsync();
        public Task DeleteAsync(Department department);
    }
}
