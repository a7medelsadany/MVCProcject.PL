using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MVCProcject.PL.ViewModels.EmployeeViewModels;
using MVCProject.BLL.DTOS;
using MVCProject.BLL.DTOS.employees;
using MVCProject.BLL.Services.Classes;
using MVCProject.BLL.Services.Interfaces;

namespace MVCProcject.PL.Controllers
{
    public class EmployeeController(IEmployeeServcies employeeServcies, ILogger<EmployeeController> ILogger, IWebHostEnvironment environment) : Controller
    {
        private readonly IEmployeeServcies _employeeServcies = employeeServcies;
        private readonly ILogger<EmployeeController> _iLogger = ILogger;
        private readonly IWebHostEnvironment _environment = environment;

        [HttpGet]
        public IActionResult Index()
        {
            //ViewData["Message"] = new DepartmentsDto { Name="hello from view data with casting"};
            //ViewBag.Message = "Hello from View Bag";
            var Employee = _employeeServcies.GetAllEmployees();
            return View(Employee);
        }

        #region create
        [HttpGet]
        public IActionResult Create(int id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateEmployeeDto createEmployeeDto)
        {
            if (ModelState.IsValid)
                try
                {
                    int result = _employeeServcies.Add(createEmployeeDto);
                    if (result > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Employee can not be Created");
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
                        ModelState.AddModelError(string.Empty, "Some thing went wrong");
                    }
                }
            return View(createEmployeeDto);
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
            };
            return View(UpdateEmployee);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int? id, EmployeeEditViewModel employeeEditViewModel)
        {
            if (!ModelState.IsValid) return View(employeeEditViewModel);
            try
            {
                var updateEmployee = new UpdateEmployeeDto()
                {
                    Id = employeeEditViewModel.Id,
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
                };
                int result = _employeeServcies.Update(updateEmployee);
                if (result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Employee can not be Updated");
                    return View(employeeEditViewModel);
                }
            }
            catch (Exception ex)
            {
                if (_environment.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View(employeeEditViewModel);
                }
                else
                {
                    _iLogger.LogError(ex.Message);
                    ModelState.AddModelError(string.Empty, "Some thing went wrong");
                    return View(employeeEditViewModel);
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
