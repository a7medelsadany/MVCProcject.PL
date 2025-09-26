using MVCProject.DAL.Models.DepartmentModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.DAL.Data.Configuration
{
    public class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T>where T:BaseEntity
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(d => d.Createdon).HasDefaultValueSql("GETDATE()");
            builder.Property(d => d.LastModificationOn).HasComputedColumnSql("GETDATE()");
        }
    }
}
