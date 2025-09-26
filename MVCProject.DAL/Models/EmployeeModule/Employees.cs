using MVCProject.DAL.Models.DepartmentModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.DAL.Models.EmployeeModule
{ 
    public class Employees:BaseEntity
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }=string.Empty;
        public int Age {  get; set; }
        public string? Address { get; set; }
        public bool IsActive { get; set; }
        public decimal Salary {  get; set; }
        public string? Email { get; set; } 
        public string? PhoneNumber { get; set; }
        public  DateTime HiringDate {  get; set; }
        public Gender Gender {  get; set; }
        public EmployeeType EmployeeType {  get; set; } 
    }
}
