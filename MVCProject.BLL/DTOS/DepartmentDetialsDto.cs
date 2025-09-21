using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.BLL.DTOS
{
    public class DepartmentDetialsDto
    {
        public int DeptId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string code { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime? DateOfCreation { get; set; }
        public int CreatedBy { get; set; }
        public int LastModificationBy { get; set; }
        public DateTime? LastModificationOn { get; set; }
        public bool isDeleted { get; set; }
    }
}
