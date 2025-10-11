using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using MVCProcject.PL.ViewModels;
using MVCProject.DAL.Models.Shared;

namespace MVCProcject.PL.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController(RoleManager<IdentityRole> _roleManager,UserManager<ApplicationUser> _userManager, IWebHostEnvironment _environment) : Controller
    {
        #region Index
        [HttpGet]
        public IActionResult Index(string searchValue)
        {
            var roleQuery = _roleManager.Roles.AsQueryable();
            if (!string.IsNullOrEmpty(searchValue))
                roleQuery = roleQuery.Where(r => r.Name.ToLower().Contains(searchValue.ToLower()));
            var roles = roleQuery.Select(r => new RoleViewModel()
            {
                Id = r.Id,
                Name = r.Name
            }).ToList();
            return View(roles);
        }
        #endregion

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(RoleViewModel roleViewModel)
        {
            if (ModelState.IsValid)
            {
                var result=_roleManager.CreateAsync(new IdentityRole() { Name= roleViewModel.Name}).Result;
                if (result.Succeeded)
                    return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Role can not be created");
            return View(roleViewModel);
        }
        #endregion

        #region Details
        [HttpGet]
        public IActionResult Details(string? id)
        {
            if (id is null) return BadRequest();
            var role = _roleManager.FindByIdAsync(id).Result;
            if (role is null) return NotFound();
            return View(new RoleViewModel()
            {
                Id = role.Id,
                Name = role.Name
            });

        }
        #endregion

        #region Edit
        [HttpGet]
        public IActionResult Edit(string? id)
        {
            if (id is null) return BadRequest();
            var role = _roleManager.FindByIdAsync(id).Result;
            if (role is null) return NotFound();
            var users = _userManager.Users.ToList();
            return View(new RoleViewModel()
            {
                Id = role.Id,
                Name = role.Name,
                Users = users.Select(user => new UserRoleViewModel {
                    userId = user.Id,
                    userName = user.UserName,
                    IsSelected = _userManager.IsInRoleAsync(user, role.Name).Result
                }).ToList()

            });
        }

        [HttpPost]
        public IActionResult Edit(string? id, RoleViewModel roleViewModel)
        {
            if (!ModelState.IsValid) return View(roleViewModel);
            if (id != roleViewModel.Id) return BadRequest();
            string message = "";
            try
            {
                var role = _roleManager.FindByIdAsync(id).Result;
                if (role is null) return NotFound();
                role.Name = roleViewModel.Name;
                var result = _roleManager.UpdateAsync(role).Result;
                foreach(var userRole in roleViewModel.Users)
                {
                    var user = _userManager.FindByIdAsync(userRole.userId).Result;
                 if(user is not null)
                    {
                        if (userRole.IsSelected && !_userManager.IsInRoleAsync(user, role.Name).Result)
                            _userManager.AddToRoleAsync(user, role.Name).Wait();
                        else if (!userRole.IsSelected && _userManager.IsInRoleAsync(user, role.Name).Result)
                            _userManager.RemoveFromRoleAsync(user, role.Name).Wait();
                    }
                }
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    message = "Role can not be updated";

            }
            catch (Exception ex)
            {
                message = _environment.IsDevelopment() ? ex.Message : "Role can not be updated!!!";
            }
            return View(roleViewModel);
        }
        #endregion

        #region Delete
        [HttpPost]
        public IActionResult Delete(string id)
        {
            if (id is null) return BadRequest();
            var role = _roleManager.FindByIdAsync(id).Result;
            if (role is null) return NotFound();
            string message = "";
            try
            {
                var result = _roleManager.DeleteAsync(role).Result;
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    message = "Role can not be deleted :(";
            }
            catch (Exception ex)
            {
                message = _environment.IsDevelopment() ? ex.Message : "Role can not be deleted!!!";
            }
            return View(nameof(Index));
        }
            #endregion
    
    }
}
