using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using MVCProcject.PL.ViewModels.AccountViewModel;
using MVCProject.BLL.Services.EmailSender;
using MVCProject.DAL.Models.Shared;
using NuGet.Packaging.Signing;
using System.ClientModel.Primitives;

namespace MVCProcject.PL.Controllers
{
    public class AccountController(UserManager<ApplicationUser> _userManager,
        SignInManager<ApplicationUser> _signInManager,
        IEmailSender _emailSender
        ) : Controller
    {
        #region Register
        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public IActionResult Register(RegisterViewModel registerView)
        {
            if (!ModelState.IsValid) return View(registerView);
            var user = new ApplicationUser()
            {
                FirstName = registerView.FirstName,
                LastName = registerView.LastName,
                UserName = registerView.UserName,
                Email = registerView.Email
            };
            var result = _userManager.CreateAsync(user, registerView.Password).Result;
            if (result.Succeeded)
            {
                return RedirectToAction("Login");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(registerView);
            }
        }
        #endregion

        #region Login
        [HttpGet]
        public IActionResult Login() => View();
        [HttpPost]
        public IActionResult Login(LoginViewModel loginView)
        {
            if (!ModelState.IsValid) return View(loginView);
            var user = _userManager.FindByEmailAsync(loginView.Email).Result;
            if (user is not null)
            {
                var flag = _userManager.CheckPasswordAsync(user, loginView.Password).Result;
                if (flag)
                {
                    var result = _signInManager.PasswordSignInAsync(user, loginView.Password, loginView.RememberMe, false).Result;
                    if (result.IsLockedOut)
                        ModelState.AddModelError("","your account is locked");
                    if(result.IsNotAllowed)
                        ModelState.AddModelError("","your account is not allowed");
                    if (result.Succeeded)
                        return RedirectToAction(nameof(HomeController.Index), "Home");
                }
            }
            ModelState.AddModelError(string.Empty, "Invalid login");
            return View(loginView);
        }
        #endregion

        #region SignOut
        [HttpGet]
        public new IActionResult SignOut()
        {
            _signInManager.SignOutAsync().GetAwaiter().GetResult();
            return RedirectToAction(nameof(Login));
        }
        #endregion

        #region ForgetPassword
            [HttpGet]
            public IActionResult ForgetPassword() => View();

            [HttpPost]
            public IActionResult ForgetPassword(ForgetPasswordViewModel forgetPassword)
            {
                if (ModelState.IsValid)
                {
                    var user=_userManager.FindByEmailAsync(forgetPassword.Email).Result;
                    if(user is not null)
                    {
                        var token = _userManager.GeneratePasswordResetTokenAsync(user).Result;
                        var url = Url.Action("ResetPassword", "Account", new { email = forgetPassword.Email,token=token }, Request.Scheme);
                        var email = new Email()
                        {
                            To = forgetPassword.Email,
                            Subject = "Reset Your Password",
                            Body = url
                        };
                        _emailSender.SendEmail(email);
                        return RedirectToAction("CheckYourInbox");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invaild Operation please try Again");
                    }
                }
                return View(forgetPassword);
            }

            [HttpGet]
            public IActionResult CheckYourInbox() => View();


        #endregion

        #region ResetPassword
        [HttpGet]
        public IActionResult ResetPassword(string email,string token)
        {
            TempData["email"] = email;
            TempData["token"] = token;
            return View();
        }

        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordViewModel resetPassword)
        {
            if (ModelState.IsValid)
            {
                var email = TempData["email"] as string;
                var token = TempData["token"] as string;
                var user = _userManager.FindByEmailAsync(email).Result;
                if (user is not null)
                {
                    var result = _userManager.ResetPasswordAsync(user, token, resetPassword.newPassword).Result;
                    if (result.Succeeded)
                        return RedirectToAction(nameof(Login));
                }
            }
            ModelState.AddModelError("", "Invalid Operation , Please Try Again");
            return View(resetPassword);
        }
        #endregion
    }
}
