using MVCProject.BLL.DTOS;
using MVCProject.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.BLL.Factorios
{
    public static class DepartmentFactory
    {
        public static DepartmentsDto ToDepartmentDto(this Department d)
        {
            return new DepartmentsDto
            {
                DeptId = d.Id,
                code = d.code,
                Name = d.Name,
                Description = d.Description,
                DateOfCreation = d.Createdon
            };
        }
        public static DepartmentDetialsDto? ToDepartmentsDetialsDto(this Department d)
        {
            return new DepartmentDetialsDto
            {
                DeptId=d.Id,
                Name=d.Name,
                Description=d.Description,
                DateOfCreation = d.Createdon,
                code=d.code,
                isDeleted=d.isDeleted,
                LastModificationBy = d.LastModificationBy,
                CreatedBy=d.CreatedBy,
                LastModificationOn=d.LastModificationOn
            };
        }

        public static Department ToEntity(this CreateDepartmentDTO createDepartmentDTO)
        {
            return new Department()
            {
                Name = createDepartmentDTO.Name,
                code = createDepartmentDTO.code,
                Description = createDepartmentDTO.Description,
                Createdon = createDepartmentDTO.DateOfCreation,
            };
        }

        public static Department ToUpdate(this UpdateDepartmentDto updateDepartmentDto)
        {
            return new Department()
            {
                Name = updateDepartmentDto.Name,
                code = updateDepartmentDto.code,
                Description = updateDepartmentDto.Description,
                Createdon = updateDepartmentDto.DateOfCreation,
            };
        }
    }
}
