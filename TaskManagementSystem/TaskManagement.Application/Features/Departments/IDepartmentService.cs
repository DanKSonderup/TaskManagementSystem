using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Features.Departments.DTO;

namespace TaskManagement.Application.Features.Departments
{
    public interface IDepartmentService
    {
        public Task<List<DepartmentDto>> GetAllDepartmentsAsync();
    }
}
