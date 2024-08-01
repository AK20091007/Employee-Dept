using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using emp_dept.DTOs;
using emp_dept.Services;
using Microsoft.AspNetCore.Mvc;

namespace emp_dept.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeDTO employeeDTO)
        {
            await _employeeService.AddEmployeeAsync(employeeDTO);
            return CreatedAtAction(nameof(GetById), new { id = employeeDTO.Id }, employeeDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, EmployeeDTO employeeDTO)
        {
            if (id != employeeDTO.Id) return BadRequest();
            await _employeeService.UpdateEmployeeAsync(employeeDTO);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _employeeService.DeleteEmployeeAsync(id);
            return NoContent();
        }
    }
}