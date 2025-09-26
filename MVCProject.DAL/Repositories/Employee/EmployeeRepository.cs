using MVCProject.DAL.Data.Context;
using MVCProject.DAL.Models.EmployeeModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.DAL.Repositories.Employee
{
    public class EmployeeRepository(ApplicationDbContext dbContext) : GenericRepository<Employees>(dbContext),IEmployeeRepository
    {
    

    }
}
