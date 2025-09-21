using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.BLL.DTOS
{
    public class DepartmentsDto
    {
        public int DeptId { get; set; }
        public string code { get; set; } = null!;
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime? DateOfCreation { get; set; }

    }
}
