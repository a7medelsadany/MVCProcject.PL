using MVCProject.BLL.DTOS.employees;

namespace MVCProject.BLL.Services.Interfaces
{
    public interface IEmployeeServcies
    {
        int Add(CreateEmployeeDto createEmployeeDto);
        IEnumerable<EmployeeDto> GetAllEmployees();
        EmployeeDetailsDto? getById(int id);
        int Update(UpdateEmployeeDto updateEmployeeDto);
        bool DeleteEmployee(int id);
    }
}