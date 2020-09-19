using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;
using ViewModels;

namespace Main.Controllers
{
    public class AccountController : Controller
    {
        private SignInManager<User> _signInManager;
        private UserManager<User> _userManager;
        public AccountController(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public IActionResult SendVerifier()
        { 
            return View();
        }
        [HttpPost]
        public IActionResult SendVerifier(string Mobile,string VerifierCode)
        {
            TempData["Mobile"] = Mobile;
            if (VerifierCode!=null)
            {
                if (VerifierCode=="1010")
                {
                    return RedirectToAction("Register");
                }
                else
                {
                    ViewData["message"] = "شماره تایید درست نمی باشد";
                }
            }
            ViewData["Verifier"] = "1010";

            return View();
        }
        public ViewResult Register()
        {
            ViewData["Mobile"] = TempData["Mobile"];
            TempData.Keep();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(ViewModels.RegisterViewModel vMRegister)
        {
            if (ModelState.IsValid)
            {
                var user = new User { UserName = vMRegister.Mobile };
                var result = await _userManager.CreateAsync(user, vMRegister.Password);
                var error= result.Errors.Where(C => C.Code == "DuplicateUserName").FirstOrDefault();
                if (error!=null)
                {
                    ViewData["DuplicateUserName"] = "شماره همراه تکراری می باشد";
                }
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Login");
                }
            
            }
            return View();
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = "")
        {
            var Mobile = TempData["Mobile"];
            TempData.Keep();
            var model = new LoginViewModel { ReturnUrl = returnUrl };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(
                    model.Mobile, model.Password, true,false);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) &&
                        Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "DisplayBooks");
                    }
                }
            }
            ModelState.AddModelError("", "نام کاربری یا گذر واژه معتبر نمی باشد");
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}