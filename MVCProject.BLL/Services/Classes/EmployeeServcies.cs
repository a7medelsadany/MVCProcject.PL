using AutoMapper;
using MVCProject.BLL.DTOS.employees;
using MVCProject.BLL.Services.AttachmentService;
using MVCProject.BLL.Services.Interfaces;
using MVCProject.DAL.Models.EmployeeModule;
using MVCProject.DAL.Repositories;
using MVCProject.DAL.Repositories.Employee;
using MVCProject.DAL.Repositories.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.BLL.Services.Classes
{
    public class EmployeeServcies(IUnitOfWork unitOfWork,IMapper mapper,IAttachmentService attachmentService) : IEmployeeServcies
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IAttachmentService _attachmentService = attachmentService;

        //Get All
        public IEnumerable<EmployeeDto> GetAllEmployees()
        {
            var employees = _unitOfWork.EmployeeRepository.GetAll();
            var employeeDtos = _mapper.Map<IEnumerable<Employees>, IEnumerable<EmployeeDto>>(employees);
            return employeeDtos;
        }

        //GetbyId
        public EmployeeDetailsDto? getById(int id)
        {
            var employee = _unitOfWork.EmployeeRepository.GetById(id);
           //var employeeDetailsDto=_mapper.Map<Employees , EmployeeDetailsDto>(employee);
           // return employeeDetailsDto;
           return employee is null ? null : _mapper.Map<Employees, EmployeeDetailsDto>(employee);
        }

       // Add new employee
        public int Add(CreateEmployeeDto createEmployeeDto)
        {
            var employee=_mapper.Map<CreateEmployeeDto, Employees>(createEmployeeDto);
            if(createEmployeeDto.Image is not null)
            {
                employee.ImageName = _attachmentService.Upload(createEmployeeDto.Image, "Images");
            }
            _unitOfWork.EmployeeRepository.Add(employee);
             return _unitOfWork.saveChanges();   
        }

        //Update employee
        public int Update(UpdateEmployeeDto updateEmployeeDto)
        {
            _unitOfWork.EmployeeRepository.Update(_mapper.Map<UpdateEmployeeDto, Employees>(updateEmployeeDto));
            return _unitOfWork.saveChanges();
        }

        public bool DeleteEmployee(int id)
        {
            var employee = _unitOfWork.EmployeeRepository.GetById(id);
            if (employee is null) return false;

            _unitOfWork.EmployeeRepository.Remove(employee);
               return _unitOfWork.saveChanges() > 0;
        }

    }
}
