using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MVCProcject.PL.ViewModels.EmployeeViewModels;
using MVCProject.BLL.DTOS;
using MVCProject.BLL.DTOS.employees;
using MVCProject.BLL.Services.Classes;
using MVCProject.BLL.Services.Interfaces;
using MVCProject.DAL.Models.DepartmentModule;
using MVCProject.DAL.Models.EmployeeModule;

namespace MVCProcject.PL.Controllers
{
    public class EmployeeController(IEmployeeServcies employeeServcies, ILogger<EmployeeController> ILogger, IWebHostEnvironment environment,IDepartmentServices departmentServices) : Controller
    {
        private readonly IEmployeeServcies _employeeServcies = employeeServcies;
        private readonly ILogger<EmployeeController> _iLogger = ILogger;
        private readonly IWebHostEnvironment _environment = environment;
        private readonly IDepartmentServices _departmentServices = departmentServices;

        [HttpGet]
        public IActionResult Index(string? EmployeeSearchName)
        {
            //ViewData["Message"] = new DepartmentsDto { Name="hello from view data with casting"};
            //ViewBag.Message = "Hello from View Bag";
            var Employee = _employeeServcies.GetAllEmployees();
            if (!string.IsNullOrWhiteSpace(EmployeeSearchName))
            {
                Employee = Employee.Where(e => e.Name.Contains(EmployeeSearchName, StringComparison.OrdinalIgnoreCase));
            }
            return View(Employee);
        }

        #region create
        [HttpGet]
        public IActionResult Create(int id)
        {
            var model = new EmployeeEditViewModel
            {
                Departments = _departmentServices.GetAllDepartments()
                    .Select(d => new SelectListItem { Value = d.DeptId.ToString(), Text = d.Name })
            };
            return View(model);
        }

        [HttpPost]
       
        public IActionResult Create(EmployeeEditViewModel EmployeeVM)
        {
            if (ModelState.IsValid) { 
                try
                {
                    var createEmployeeDto = new CreateEmployeeDto
                    {
                        Name = EmployeeVM.Name,
                        Address = EmployeeVM.Address,
                        DepartId=EmployeeVM.DepartId,
                        Age = EmployeeVM.Age,
                        Salary = EmployeeVM.Salary,
                        Email = EmployeeVM.Email,
                        employeeType=EmployeeVM.employeeType,
                        gender=EmployeeVM.gender,
                        HiringDate=EmployeeVM.HiringDate,
                        PhoneNumber=EmployeeVM.phoneNumber,
                        Image= EmployeeVM.Image,
                    };
                    int result = _employeeServcies.Add(createEmployeeDto);
                    
                    if (result > 0)
                    {
                        return RedirectToAction(nameof(Index));

                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "employee can not be created!!");

                    }
                }
                catch (Exception ex)
                {
                    if (_environment.IsDevelopment())
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                    else
                    {
                        _iLogger.LogError(ex.Message);
                    }
                }

            }
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage).ToList();
                ViewBag.Errors = errors;
                EmployeeVM.Departments =
                    _departmentServices.GetAllDepartments()
                    .Select(d => new SelectListItem { Value = d.DeptId.ToString(), Text = d.Name });
            }
            return View (EmployeeVM);
        }

        #endregion

        #region Details
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var employee = _employeeServcies.getById(id.Value);
            if (employee is null) return NotFound();
            return View(employee);

        }
        #endregion
        #region Edit
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var employee = _employeeServcies.getById(id.Value);
            if (employee is null) return NotFound();
            var UpdateEmployee = new EmployeeEditViewModel
            {
                Id = employee.Id,
                Name = employee.Name,
                Age = employee.Age,
                Address = employee.Address,
                IsActive = employee.IsActive,
                Salary = employee.Salary,
                Email = employee.Email,
                phoneNumber = employee.phoneNumber,
                HiringDate = employee.HiringDate,
                CreatedBy = employee.CreatedBy,
                employeeType = employee.employeeType,
                Createdon = employee.Createdon,
                LastModificationBy = employee.LastModificationBy,
                LastModificationOn = employee.LastModificationOn,
                gender = employee.gender,
                DepartId= employee.DepartId,
            };
            UpdateEmployee.Departments = _departmentServices.GetAllDepartments()
                .Select(d => new SelectListItem 
                {
                    Value = d.DeptId.ToString(), 
                    Text = d.Name,
                    Selected=d.DeptId==employee.Id
                });
            return View(UpdateEmployee);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int? id, EmployeeEditViewModel employeeEditViewModel)
        {
            if(!id.HasValue) return BadRequest();
            if (!ModelState.IsValid)
            {
                employeeEditViewModel.Departments = _departmentServices.GetAllDepartments()
               .Select(d => new SelectListItem
               {
                   Value = d.DeptId.ToString(),
                   Text = d.Name,
                   Selected = d.DeptId == employeeEditViewModel.Id
               });
                return View(employeeEditViewModel);
            }
            try
            {
                var updateEmployee = new UpdateEmployeeDto()
                {
                    Id = id.Value,
                    Name = employeeEditViewModel.Name,
                    Age = employeeEditViewModel.Age,
                    Address = employeeEditViewModel.Address,
                    IsActive = employeeEditViewModel.IsActive,
                    Salary = employeeEditViewModel.Salary,
                    Email = employeeEditViewModel.Email,
                    phoneNumber = employeeEditViewModel.phoneNumber,
                    HiringDate = employeeEditViewModel.HiringDate,
                    CreatedBy = employeeEditViewModel.CreatedBy,
                    employeeType = employeeEditViewModel.employeeType,
                    Createdon = employeeEditViewModel.Createdon,
                    LastModificationBy = employeeEditViewModel.LastModificationBy,
                    LastModificationOn = DateTime.Now,
                    DepartId= employeeEditViewModel.DepartId,
                };
                var result = _employeeServcies.Update(updateEmployee);
                if (result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                    ModelState.AddModelError(string.Empty, "Employee can not be Updated");
                    employeeEditViewModel.Departments = _departmentServices.GetAllDepartments()
                     .Select(d => new SelectListItem
                        {
                          Value = d.DeptId.ToString(),
                          Text = d.Name,
                          Selected = d.DeptId == employeeEditViewModel.Id
                        });
                 return View(employeeEditViewModel);

            }
            catch (Exception ex)
            {
                if (_environment.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    employeeEditViewModel.Departments = _departmentServices.GetAllDepartments()
                    .Select(d => new SelectListItem
                        {
                            Value = d.DeptId.ToString(),
                            Text = d.Name,
                            Selected = d.DeptId == employeeEditViewModel.Id
                        });
                    return View(employeeEditViewModel);
                }
                else
                {
                    _iLogger.LogError(ex.Message);
                    return View("ErrorView", ex);
                }
            }
        }
        #endregion

        #region Delete
        [HttpPost]
        public IActionResult Delete(int id)
        { 
            if (id==0) return BadRequest();
            try
            {
                var result = _employeeServcies.DeleteEmployee(id);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "employee can not be Deleted");
                    return RedirectToAction(nameof(Delete), new {id=id});
                }
            }
            catch (Exception ex)
            {
                if (_environment.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _iLogger.LogError(ex.Message);
                    return Content("Some thing went wrong");
                }
            }
        }
        #endregion





    }
}
