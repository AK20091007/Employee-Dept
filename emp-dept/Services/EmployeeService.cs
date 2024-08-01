using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using emp_dept.DTOs;
using emp_dept.Repositories;
using emp_dept.Utilities;
using Microsoft.Extensions.Caching.Memory;
using Models;

namespace emp_dept.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;
        private const string CacheKey = "EmployeeList";

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper, IMemoryCache cache)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<IEnumerable<EmployeeDTO>> GetAllEmployeesAsync()
        {
            if (!_cache.TryGetValue(CacheKey, out IEnumerable<EmployeeDTO> employeeDTOs))
            {
                var employees = await _employeeRepository.GetAllAsync();
                employeeDTOs = _mapper.Map<IEnumerable<EmployeeDTO>>(employees);

                var cacheOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
                    SlidingExpiration = TimeSpan.FromMinutes(2)
                };

                _cache.Set(CacheKey, employeeDTOs, cacheOptions);
            }

            return employeeDTOs;
        }

        public async Task<EmployeeDTO> GetEmployeeByIdAsync(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            return _mapper.Map<EmployeeDTO>(employee);
        }

        public async Task AddEmployeeAsync(EmployeeDTO employeeDTO)
        {
            var employee = _mapper.Map<Employee>(employeeDTO);
            await _employeeRepository.AddAsync(employee);
            _cache.Remove(CacheKey); // Invalidate cache
        }

        public async Task UpdateEmployeeAsync(EmployeeDTO employeeDTO)
        {
            var employee = _mapper.Map<Employee>(employeeDTO);
            await _employeeRepository.UpdateAsync(employee);
            _cache.Remove(CacheKey); // Invalidate cache
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            await _employeeRepository.DeleteAsync(id);
            _cache.Remove(CacheKey); // Invalidate cache
        }

        public async Task<PaginatedList<EmployeeDTO>> GetEmployeesAsync(int pageIndex, int pageSize)
        {
            var source = _employeeRepository.GetAllAsync().Result.AsQueryable();
            var paginatedList = await PaginatedList<Employee>.CreateAsync(source, pageIndex, pageSize);
            return _mapper.Map<PaginatedList<EmployeeDTO>>(paginatedList);
        }
    }

}