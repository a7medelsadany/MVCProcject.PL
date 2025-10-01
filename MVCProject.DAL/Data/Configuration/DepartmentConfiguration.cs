using MVCProject.DAL.Models.DepartmentModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MVCProject.DAL.Data.Configuration
{
    public class DepartmentConfiguration : BaseEntityConfiguration<Department>,IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(d => d.Id).UseIdentityColumn(10, 10);
            builder.Property(d => d.Name).HasColumnType("varchar(20)");
            builder.Property(d => d.code).HasColumnType("varchar(10)");
            builder.HasMany(D => D.Employees)
                .WithOne(E => E.Department)
                .HasForeignKey(E => E.DepartId)
                .OnDelete(DeleteBehavior.SetNull);

            base.Configure(builder);
          
        }
    }
}
