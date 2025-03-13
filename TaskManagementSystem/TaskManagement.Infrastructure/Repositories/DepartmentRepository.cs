using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Features.Departments.DTO;
using TaskManagement.Application.Interfaces;
using TaskManagement.Domain.Entities;
using TaskManagement.Infrastructure.Data;

namespace TaskManagement.Infrastructure.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDbContext _context;

        public DepartmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Department department)
        {
            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Department department)
        {
            var foundDepartment = await _context.Departments.FindAsync(department.Id);
            if (foundDepartment != null) {
                _context.Departments.Remove(foundDepartment);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public Task<IEnumerable<Department>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<DepartmentDto>> GetAllDepartmentsAsync()
        {
            return await _context.Departments.Select(d => new DepartmentDto
            {
                Id = d.Id,
                Name = d.Name
            }).ToListAsync();
        }

        public async Task<Department?> GetByIdAsync(Guid departmentId)
        {
            return await _context.Departments.FindAsync(departmentId);
        }

        public Task<Department> UpdateAsync(Department department)
        {
            throw new NotImplementedException();
        }
    }
}
