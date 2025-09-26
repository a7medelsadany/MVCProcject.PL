using AutoMapper;
using AutoMapper.Configuration.Conventions;
using MVCProject.BLL.DTOS;
using MVCProject.BLL.DTOS.employees;
using MVCProject.DAL.Models.DepartmentModule;
using MVCProject.DAL.Models.EmployeeModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.BLL
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Department, DepartmentsDto>()
                .ForMember(dest=>dest.DeptId,option=>option.MapFrom(src=>src.Id));
            CreateMap<Department, DepartmentDetialsDto>();
            CreateMap<CreateDepartmentDTO, Department>();
            CreateMap<UpdateDepartmentDto, Department>()
                .ForMember(dest => dest.Id, option => option.MapFrom(src => src.DeptId));


            CreateMap<Employees, EmployeeDto>();
            CreateMap<Employees, EmployeeDetailsDto>()
                .ForMember(dest=>dest.phoneNumber,opt=>opt.MapFrom(src=>src.PhoneNumber));

            CreateMap<CreateEmployeeDto, Employees>();
            CreateMap<UpdateEmployeeDto, Employees>();
        }
    }
}
