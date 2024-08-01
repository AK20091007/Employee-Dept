using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using emp_dept.DTOs;

namespace emp_dept.Services
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentDTO>> GetAllDepartmentsAsync();
        Task<DepartmentDTO> GetDepartmentByIdAsync(int id);
        Task AddDepartmentAsync(DepartmentDTO departmentDTO);
        Task UpdateDepartmentAsync(DepartmentDTO departmentDTO);
        Task DeleteDepartmentAsync(int id);
    }
}