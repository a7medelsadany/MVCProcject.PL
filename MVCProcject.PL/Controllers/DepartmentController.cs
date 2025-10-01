    using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MVCProcject.PL.ViewModels.DepartmentsViewModels;
using MVCProject.BLL.DTOS;
using MVCProject.BLL.Services.Interfaces;
using MVCProject.DAL.Models;

namespace MVCProcject.PL.Controllers
{
    public class DepartmentController(IDepartmentServices departmentServices, ILogger<DepartmentController> logger,IWebHostEnvironment environment) : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var department = departmentServices.GetAllDepartments();
            return View(department);
        }
        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateDepartmentDTO departmentDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int result = departmentServices.AddDepartment(departmentDTO);
                    if (result > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Department can not be Created!");

                    }

                }
                catch (Exception ex)
                {
                    //Development Enviroment 
                    if (environment.IsDevelopment())
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                       
                    }

                    //Deployment Envrioment
                    else
                    {
                        logger.LogError(ex.Message);
                        
                    }
                }


            }
            return View(departmentDTO);
        }
        #endregion

        #region Details
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (!id.HasValue) return BadRequest(); //Error 400
            var department = departmentServices.GetDepartmentById(id.Value);
            if (department is null) return NotFound();
            else
                return View(department);
        }
        #endregion

        #region Edit
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if(!id.HasValue) return BadRequest();
            var department = departmentServices.GetDepartmentById(id.Value);
            if (department is null) return NotFound();
            var DepartmentViewModel = new DepartmentEditViewModel()
            {
                code=department.code,
                Name=department.Name,
                Description=department.Description,
                DateOfCreation=department.DateOfCreation
            };
            return View(DepartmentViewModel);
        }

        [HttpPost]
        public IActionResult Edit(DepartmentEditViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);
            try
            {
                var UpdateDepartment = new UpdateDepartmentDto()
                {
                    DeptId = viewModel.Id,
                    code=viewModel.code,
                    Name=viewModel.Name,
                    Description =viewModel.Description,
                    DateOfCreation=viewModel.DateOfCreation
                };
                int result = departmentServices.UpdateDepartment(UpdateDepartment);
                if (result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Department can not be Updated");
                    return View(viewModel);
                }


            }
            catch (Exception ex) {
                if (environment.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View(viewModel);
                }
                else
                {
                    logger.LogError(ex.Message);
                    return View("errorView",ex);
                }
            }
        }
        #endregion

        #region Delete
        //[HttpGet]
        //public IActionResult Delete(int? id)
        //{
        //    if (!id.HasValue) return BadRequest();
        //    var department = departmentServices.GetDepartmentById(id.Value);
        //    if (department is null) return NotFound();
        //    return View(department);

        //}

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (id == 0) return BadRequest();
            try
            {
                bool Deleted = departmentServices.DeleteDepartment(id);
                if (Deleted)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Department is not deleted ");
                    return RedirectToAction(nameof(Delete), new { id });
                }

            }
            catch (Exception ex)
            {
                if (environment.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    logger.LogError(ex.Message);
                    return View("ErrorView", ex);
                }
            }
        }
        #endregion

    }
}
