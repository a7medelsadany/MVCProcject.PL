using MVCProject.BLL.DTOS;

namespace MVCProject.BLL.Services.Interfaces
{
    public interface IDepartmentServices
    {
        int AddDepartment(CreateDepartmentDTO DepartmentDTO);
        bool DeleteDepartment(int id);
        IEnumerable<DepartmentsDto> GetAllDepartments();
        DepartmentDetialsDto? GetDepartmentById(int id);
        int UpdateDepartment(UpdateDepartmentDto updateDepartmentDto);
    }
}