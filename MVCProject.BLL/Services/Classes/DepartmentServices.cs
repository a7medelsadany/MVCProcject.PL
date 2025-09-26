using AutoMapper;
using Azure.Core;
using MVCProject.BLL.DTOS;

using MVCProject.BLL.Services.Interfaces;
using MVCProject.DAL.Models;
using MVCProject.DAL.Models.DepartmentModule;
using MVCProject.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.BLL.Services.Classes
{
    public class DepartmentServices(IDepartmentRepository departmentRepository,IMapper mapper) : IDepartmentServices
    {
        private readonly IMapper _mapper = mapper;

        //Get All
        public IEnumerable<DepartmentsDto> GetAllDepartments()
        {
            var departments = departmentRepository.GetAll();
            return _mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentsDto>>(departments);
        }


        //getById
        public DepartmentDetialsDto? GetDepartmentById(int id)
        {
            var department = departmentRepository.GetById(id);
            return department is null ? null : _mapper.Map<Department, DepartmentDetialsDto>(department);
        }


        //Add
        public int AddDepartment(CreateDepartmentDTO DepartmentDTO)
        {
            return departmentRepository.Add(_mapper.Map<CreateDepartmentDTO, Department>(DepartmentDTO));
        }

        //Update
        public int UpdateDepartment(UpdateDepartmentDto updateDepartmentDto)
        {
            return departmentRepository.Update(_mapper.Map<UpdateDepartmentDto, Department>(updateDepartmentDto));
        }

        //Remove مفيش mapping

        public bool DeleteDepartment(int id)
        {
            var department = departmentRepository.GetById(id);
            if (department is null)
                return false;
            int numOfRows = departmentRepository.Remove(department);
            return numOfRows > 0 ? true : false;

        }
    }
}
