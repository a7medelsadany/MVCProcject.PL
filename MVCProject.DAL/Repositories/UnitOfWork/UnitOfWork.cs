using MVCProject.DAL.Data.Context;
using MVCProject.DAL.Repositories.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.DAL.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Lazy<IDepartmentRepository> _departmentRepository;
        private readonly Lazy<IEmployeeRepository> _employeeRepository;
        private readonly ApplicationDbContext _dbContext;
        public UnitOfWork(IDepartmentRepository departmentRepository,IEmployeeRepository employeeRepository,ApplicationDbContext dbContext)
        {
            _departmentRepository = new Lazy<IDepartmentRepository>(() => new DepartmentRepository(dbContext));
            _employeeRepository = new Lazy<IEmployeeRepository>(() => new EmployeeRepository(dbContext));
            _dbContext = dbContext;
        }
        public IDepartmentRepository DepartmentRepository => _departmentRepository.Value;

        public IEmployeeRepository EmployeeRepository => _employeeRepository.Value;

        public int saveChanges()
        {
            return _dbContext.SaveChanges();
        }
    }
}
