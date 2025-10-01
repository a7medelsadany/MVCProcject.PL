using AutoMapper;
using Azure.Core;
using MVCProject.BLL.DTOS;

using MVCProject.BLL.Services.Interfaces;
using MVCProject.DAL.Models;
using MVCProject.DAL.Models.DepartmentModule;
using MVCProject.DAL.Repositories;
using MVCProject.DAL.Repositories.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.BLL.Services.Classes
{
    public class DepartmentServices(IUnitOfWork unitOfWork,IMapper mapper) : IDepartmentServices
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        //Get All
        public IEnumerable<DepartmentsDto> GetAllDepartments()
        {
            var departments = _unitOfWork.DepartmentRepository.GetAll();
            return _mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentsDto>>(departments);
        }


        //getById
        public DepartmentDetialsDto? GetDepartmentById(int id)
        {
            var department = _unitOfWork.DepartmentRepository.GetById(id);
            return department is null ? null : _mapper.Map<Department, DepartmentDetialsDto>(department);
        }


        //Add
        public int AddDepartment(CreateDepartmentDTO DepartmentDTO)
        {
            _unitOfWork.DepartmentRepository.Add(_mapper.Map<CreateDepartmentDTO, Department>(DepartmentDTO));
            return _unitOfWork.saveChanges();
        }

        //Update
        public int UpdateDepartment(UpdateDepartmentDto updateDepartmentDto)
        {
            _unitOfWork.DepartmentRepository.Update(_mapper.Map<UpdateDepartmentDto, Department>(updateDepartmentDto));
            return _unitOfWork.saveChanges();
        }

        //Remove مفيش mapping

        public bool DeleteDepartment(int id)
        {
            var department = _unitOfWork.DepartmentRepository.GetById(id);
            if (department is null)
                return false;
            _unitOfWork.DepartmentRepository.Remove(department);
            return _unitOfWork.saveChanges() > 0 ? true : false;

        }
    }
}
