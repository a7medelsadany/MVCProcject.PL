using MVCProject.DAL.Models.EmployeeModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.BLL.DTOS.employees
{
    public class EmployeeDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Age { get; set; }
        public string? Address { get; set; }
        public bool IsActive { get; set; }
        public decimal Salary { get; set; }
        public string? Email { get; set; }
        public string phoneNumber { get; set; } = null!;
        public DateTime HiringDate { get; set; }
        public Gender gender { get; set; }
        public EmployeeType employeeType { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? Createdon { get; set; }
        public int LastModificationBy { get; set; }
        public DateTime? LastModificationOn { get; set; }
    }
}
