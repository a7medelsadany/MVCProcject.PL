//using MVCProject.BLL.DTOS.employees;
//using MVCProject.DAL.Models.EmployeeModule;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace MVCProject.BLL.Factorios
//{
//    public static class EmployeeFactory
//    {
//        public static EmployeeDto ToEmployeeDto(this Employees e)
//        {
//            return new EmployeeDto
//            {
//                Name = e.Name,
//                Salary = e.Salary,
//                Address = e.Address,
//                Age = e.Age,
//                Email = e.Email,
//                employeeType = e.EmployeeType,
//                IsActive = e.IsActive,
//                gender = e.Gender
//            };
//        }
//        public static EmployeeDetailsDto ToEmployeeDetailsDto(this Employees e)
//        {
//            return new EmployeeDetailsDto
//            {
//                Name = e.Name,
//                Salary = e.Salary,
//                Address = e.Address,
//                Age = e.Age,
//                Email = e.Email,
//                HiringDate = e.HiringDate,
//                employeeType = e.EmployeeType,
//                gender = e.Gender,
//                IsActive = e.IsActive,
//                phoneNumber = e.PhoneNumber
//            };
//        }
//        public static Employees ToAdd(this CreateEmployeeDto createEmployeeDto)
//        {
//            return new Employees
//            {
//                Name = createEmployeeDto.Name,
//                Salary = createEmployeeDto.Salary,
//                Address = createEmployeeDto.Address,
//                Age = createEmployeeDto.Age,
//                Email = createEmployeeDto.Email,
//                HiringDate = createEmployeeDto.HiringDate,
//                CreatedBy = createEmployeeDto.CreatedBy,
//                EmployeeType = createEmployeeDto.employeeType,
//                Gender = createEmployeeDto.gender,
//                IsActive = createEmployeeDto.IsActive,
//                PhoneNumber = createEmployeeDto.PhoneNumber,
//                LastModificationBy = createEmployeeDto.LastModificationBy
//            };
//        }
//        public static Employees ToUpdate(this UpdateEmployeeDto updateEmployeeDto)
//        {

//            return new Employees
//            {
//                Name= updateEmployeeDto.Name,
//                HiringDate= updateEmployeeDto.HiringDate,
//                Salary= updateEmployeeDto.Salary,
//                Address= updateEmployeeDto.Address,
//                Age= updateEmployeeDto.Age,
//                Email = updateEmployeeDto.Email,
//                CreatedBy= updateEmployeeDto.CreatedBy,
//                EmployeeType = updateEmployeeDto.employeeType, 
//                Gender = updateEmployeeDto.gender,
//                IsActive= updateEmployeeDto.IsActive,
//                LastModificationBy= updateEmployeeDto.LastModificationBy,
//                PhoneNumber= updateEmployeeDto.phoneNumber,

//            };
//        }
//    }
//}
