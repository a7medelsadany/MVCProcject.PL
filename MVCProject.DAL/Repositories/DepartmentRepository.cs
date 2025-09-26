using MVCProject.DAL.Data.Context;
using MVCProject.DAL.Models.DepartmentModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.DAL.Repositories
{
    public class DepartmentRepository(ApplicationDbContext dbContext) : GenericRepository<Department>(dbContext),IDepartmentRepository
    {

        // CRUD Operations
        //get all
       
    }
}
