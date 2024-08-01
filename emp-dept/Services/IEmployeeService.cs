using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using emp_dept.DTOs;
using emp_dept.Utilities;

namespace emp_dept.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDTO>> GetAllEmployeesAsync();
        Task<EmployeeDTO> GetEmployeeByIdAsync(int id);
        Task AddEmployeeAsync(EmployeeDTO employeeDTO);
        Task UpdateEmployeeAsync(EmployeeDTO employeeDTO);
        Task DeleteEmployeeAsync(int id);
        Task<PaginatedList<EmployeeDTO>> GetEmployeesAsync(int pageIndex, int pageSize);
    }
}