using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace emp_dept.DTOs
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public int DepartmentId { get; set; }
    }
}