using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MVCProcject.PL.ViewModels;
using MVCProject.DAL.Models.Shared;

namespace MVCProcject.PL.Controllers
{
    [Authorize(Roles ="Admin")]
    public class UserController(UserManager<ApplicationUser> _userManager,IWebHostEnvironment _environment) : Controller
    {
        #region Index
        [HttpGet]
        public IActionResult Index(string searchValue)
        {
            var userQuery = _userManager.Users.AsQueryable();
            if (!string.IsNullOrEmpty(searchValue))
                userQuery = userQuery.Where(u => u.Email.ToLower().Contains(searchValue.ToLower()));
            var users = userQuery.Select(u => new UserViewModel()
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email
            }).ToList();
            foreach (var user in users)
                user.Roles = _userManager.GetRolesAsync(_userManager.FindByIdAsync(user.Id).Result).Result;
            return View(users);
        }
        #endregion

        #region Details
        
        [HttpGet]
        public IActionResult Details(string? id)
        {
            if (id is null) return BadRequest();
            var user = _userManager.FindByIdAsync(id).Result;
            if (user is null) return NotFound();
            var userVM = new UserViewModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Roles = _userManager.GetRolesAsync(user).Result

            };
            return View(userVM);
        }
        #endregion

        #region Edit
        [HttpGet]
        public IActionResult Edit(string? id) {
            if(id is null) return BadRequest();
            var user=_userManager.FindByIdAsync(id).Result;
            if (user is null) return NotFound();
            return View(new UserViewModel()
            {
                Id= user.Id,
                FirstName= user.FirstName,
                LastName= user.LastName,
                Email=user.Email,
                Roles=_userManager.GetRolesAsync(user).Result
            });
        }

        [HttpPost]
        public IActionResult Edit(UserViewModel userViewModel, string? id) { 
            if (!ModelState.IsValid) return View(userViewModel);
            if(userViewModel.Id!=id)return BadRequest();
            string message = "";
            try
            {
                var user = _userManager.FindByIdAsync(id).Result;
                if (user is null) return NotFound();
                user.FirstName = userViewModel.FirstName;
                user.LastName = userViewModel.LastName;
                user.Email = userViewModel.Email;
                var result=_userManager.UpdateAsync(user).Result;
                if (result.Succeeded)
                    return RedirectToAction(nameof(Index));
                else
                    message = "user can not be update :(";
            }
            catch (Exception ex)
            {
                message=_environment.IsDevelopment()? ex.Message : "user can not be update!!! ";
            }
            ModelState.AddModelError("", message);
            return View(userViewModel);
        }
        #endregion

        #region Delete
        [HttpPost]
        public IActionResult Delete(string? id)
        {
            var user = _userManager.FindByIdAsync(id).Result;
            if(user is null) return BadRequest();
            string message = "";
            try
            {
                var result=_userManager.DeleteAsync(user).Result;
                if (result.Succeeded)
                    return RedirectToAction(nameof(Index));
                else
                    message = "user Can Not Be Deleted!!";
            }catch (Exception ex)
            {
                message = _environment.IsDevelopment() ? ex.Message : "user can be delete";
            }
            ModelState.AddModelError("", message);
            return View(nameof(Index));
        }
        #endregion
    }
}
