using MVCProject.BLL.DTOS;
using MVCProject.BLL.Factorios;
using MVCProject.BLL.Services.Interfaces;
using MVCProject.DAL.Models;
using MVCProject.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.BLL.Services.Classes
{
    public class DepartmentServices(IDepartmentRepository departmentRepository) : IDepartmentServices
    {
        //Get All
        public IEnumerable<DepartmentsDto> GetAllDepartments()
        {
            var departments = departmentRepository.GetAll();
            return departments.Select(d => d.ToDepartmentDto());
        }


        //getById
        public DepartmentDetialsDto? GetDepartmentById(int id)
        {
            var department = departmentRepository.GetById(id);
            return department is null ? null : department.ToDepartmentsDetialsDto();
        }


        //Add
        public int AddDepartment(CreateDepartmentDTO DepartmentDTO)
        {
            return departmentRepository.Add(DepartmentDTO.ToEntity());
        }

        //Update
        public int UpdateDepartment(UpdateDepartmentDto updateDepartmentDto)
        {
            return departmentRepository.Update(updateDepartmentDto.ToUpdate());
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
