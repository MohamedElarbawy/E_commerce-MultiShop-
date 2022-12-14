using CoreLayer.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_Layer.Models;

namespace MVC_Layer.Controllers.AdminControllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly ILogger<AccountController> logger;
        private readonly IMailService mailService;

        public AccountController(UserManager<IdentityUser> userManager ,SignInManager<IdentityUser> signInManager,ILogger<AccountController> logger,IMailService mailService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
            this.mailService = mailService;
        }

        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LogIn(LogInViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index","Admin");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Email or Password");
                }

            }
            return View(model);
        }

      
        public async Task<IActionResult> LogOut()
        {
           await signInManager.SignOutAsync();
            return RedirectToAction("LogIn");
        }

        public IActionResult CreatAccount()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreatAccount(RegisterationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser()
                {
                    UserName = model.Email,
                    Email = model.Email,

                };

           var result= await userManager.CreateAsync(user,model.Password);
                if(result.Succeeded)
                {
                    return RedirectToAction("LogIN");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }

            }
            return View(model);

        }
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user= await userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var token = await userManager.GeneratePasswordResetTokenAsync(user);
                    var passwordResetLink = Url.Action("ResetPassword", "Account", new { Email = model.Email, Token = token }, Request.Scheme);
                    logger.Log(LogLevel.Warning, passwordResetLink);
                    mailService.SendEmail(model.Email, "reset password", passwordResetLink);
                    return RedirectToAction("ConfirmForgetPassword");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Email");
                    return View(model);

                }
            }
            return View(model);
        }


        public IActionResult ConfirmForgetPassword()
        {
            return View();
        }

        public IActionResult ResetPassword(string Email, string Token)
        {
            if(Email == null||Token==null)
            {
                ModelState.AddModelError("", "Invalid Data");
            }
            return View();
        }
        [HttpPost]
        public async  Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user !=null)
                {
                    var result = await userManager.ResetPasswordAsync(user, model.Token, model.Password);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("ConfirmPasswordReset");
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(model);
                }
                return RedirectToAction("ConfirmPasswordReset");

            }
            return View(model);
        }


        public IActionResult ConfirmPasswordReset()
        {
            return View();
        }



    }
}
