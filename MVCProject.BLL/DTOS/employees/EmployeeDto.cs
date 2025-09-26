using MVCProject.DAL.Models.EmployeeModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.BLL.DTOS.employees
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Age {  get; set; }
        public string? Address { get; set; }
        public bool IsActive {  get; set; }
        public decimal Salary {  get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public Gender gender { get; set; }
        public EmployeeType employeeType { get; set; }
    }
}
