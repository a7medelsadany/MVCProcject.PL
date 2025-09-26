using AutoMapper;
using MVCProject.BLL.DTOS.employees;

using MVCProject.BLL.Services.Interfaces;
using MVCProject.DAL.Models.EmployeeModule;
using MVCProject.DAL.Repositories;
using MVCProject.DAL.Repositories.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.BLL.Services.Classes
{
    public class EmployeeServcies(IEmployeeRepository employeeRepository,IMapper mapper) : IEmployeeServcies
    {
        private readonly IMapper _mapper = mapper;

        //Get All
        public IEnumerable<EmployeeDto> GetAllEmployees()
        {
            var employees = employeeRepository.GetAll();
            var employeeDtos = _mapper.Map<IEnumerable<Employees>, IEnumerable<EmployeeDto>>(employees);
            return employeeDtos;
        }

        //GetbyId
        public EmployeeDetailsDto? getById(int id)
        {
            var employee = employeeRepository.GetById(id);
           //var employeeDetailsDto=_mapper.Map<Employees , EmployeeDetailsDto>(employee);
           // return employeeDetailsDto;
           return employee is null ? null : _mapper.Map<Employees, EmployeeDetailsDto>(employee);
        }

       // Add new employee
        public int Add(CreateEmployeeDto createEmployeeDto)
        {
            return employeeRepository.Add(_mapper.Map<CreateEmployeeDto, Employees>(createEmployeeDto));
        }

        //Update employee
        public int Update(UpdateEmployeeDto updateEmployeeDto)
        {
            return employeeRepository.Update(_mapper.Map<UpdateEmployeeDto, Employees>(updateEmployeeDto));
        }

        public bool DeleteEmployee(int id)
        {
            var employee = employeeRepository.GetById(id);
            if (employee is null) return false;

            return employeeRepository.Remove(employee) > 0;
        }

    }
}
