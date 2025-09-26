using MVCProject.DAL.Models.EmployeeModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.DAL.Data.Configuration
{
    public class EmployeeConfiguration : BaseEntityConfiguration<Employees>,IEntityTypeConfiguration<Employees>
    {
        public void Configure(EntityTypeBuilder<Employees> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.EmployeeType)
                .HasConversion((EmpType) => EmpType.ToString(),
                 (_type) => (EmployeeType)Enum.Parse(typeof(EmployeeType), _type));

            builder.Property(e => e.Gender)
                .HasConversion((EmployeeGender) => EmployeeGender.ToString(),
                    (_gender) => (Gender)Enum.Parse(typeof(Gender), _gender));

            base.Configure(builder);



        }
    }
}
