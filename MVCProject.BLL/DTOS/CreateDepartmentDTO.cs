using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.BLL.DTOS
{
    public class CreateDepartmentDTO
    {
        [Required (ErrorMessage ="Name is requird!!!")]
        public string Name { get; set; } = string.Empty!;
        [Required(ErrorMessage = "code is requird!!!")]
        [Range(100,int.MaxValue)]
        public string code { get; set; } = string.Empty!;
        public string? Description { get; set; }
        public DateTime DateOfCreation { get; set; }
    }
}
