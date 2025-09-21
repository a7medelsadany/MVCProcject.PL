using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.DAL.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? Createdon { get; set; }
        public int LastModificationBy { get; set; }
        public DateTime? LastModificationOn { get; set; }
        public bool isDeleted { get; set; }
    }
}
