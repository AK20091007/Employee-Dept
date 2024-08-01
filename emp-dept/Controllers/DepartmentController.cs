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
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var departments = await _departmentService.GetAllDepartmentsAsync();
            return Ok(departments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var department = await _departmentService.GetDepartmentByIdAsync(id);
            return Ok(department);
        }

        [HttpPost]
        public async Task<IActionResult> Create(DepartmentDTO departmentDTO)
        {
            await _departmentService.AddDepartmentAsync(departmentDTO);
            return CreatedAtAction(nameof(GetById), new { id = departmentDTO.Id }, departmentDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, DepartmentDTO departmentDTO)
        {
            if (id != departmentDTO.Id) return BadRequest();
            await _departmentService.UpdateDepartmentAsync(departmentDTO);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _departmentService.DeleteDepartmentAsync(id);
            return NoContent();
        }
    }
}