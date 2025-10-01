using Microsoft.AspNetCore.Mvc.Rendering;
using MVCProject.DAL.Models.EmployeeModule;
using System.ComponentModel.DataAnnotations;

namespace MVCProcject.PL.ViewModels.EmployeeViewModels
{
    public class EmployeeEditViewModel
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
        [Display(Name ="Department Name")]
        public int? DepartId { get; set; }
        public IEnumerable<SelectListItem>? Departments { get; set; }
        public IFormFile? Image { get; set; }
    }
}
