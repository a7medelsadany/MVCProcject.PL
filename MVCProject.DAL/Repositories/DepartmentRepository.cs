using MVCProject.DAL.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.DAL.Repositories
{
    public class DepartmentRepository(ApplicationDbContext dbContext) : IDepartmentRepository
    {

        // CRUD Operations
        //get all
        public IEnumerable<Department> GetAll(bool withTracking = false)
        {
            if (withTracking)
            {
                return dbContext.Departments.ToList();
            }
            else
            {
                return dbContext.Departments.AsNoTracking().ToList();
            }
        }
        //get by id
        public Department? GetById(int id)
        => dbContext.Departments.Find(id);

        //add
        public int Add(Department department)
        {
            dbContext.Departments.Add(department);
            return dbContext.SaveChanges();
        }

        //update
        public int Update(Department department)
        {
            dbContext.Departments.Update(department);
            return dbContext.SaveChanges();
        }

        //Delete or Remove
        public int Remove(Department department)
        {
            dbContext.Departments.Remove(department);
            return dbContext.SaveChanges();
        }
    }
}
