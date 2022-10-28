using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SoftWizBusinessLogic.DTOs;
using SoftWizBusinessLogic.Interfaces;
using SoftWizDataAccess.Models;
using SoftWizECommerce.AuthService;
using SoftWizECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftWizECommerce.Controllers
{
    public class AccountController : Controller
    {
        private IAccountService _accountService { get; set; }
        private ITokenService _tokenService { get; set; }
        private IConfiguration _configuration { get; set; }

        public AccountController(IAccountService accountservice, ITokenService tokenservice, IConfiguration configuration)
        {
            _accountService = accountservice;
            _tokenService = tokenservice;
            _configuration = configuration;
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
                {
                    return Json(new { flag = false, message = "Both email and password is required" });
                }

                var user = _accountService.GetUser(model.Email, model.Password);

                if (user != null)
                {
                    var generatedToken = _tokenService.BuildToken(
                        _configuration["Jwt:Key"].ToString(),
                        _configuration["Jwt:Issuer"].ToString(),
                        _configuration["Jwt:Audience"].ToString(),
                        user);

                    if (generatedToken != null)
                    {
                        HttpContext.Session.SetString("Token", generatedToken);
                        return Json(new { flag = true, type = "redirect", message = "/Home/Index" });
                    }
                    else
                    {
                        return Json(new { flag = false, message = "Some error has occurred, please contact the administrator" });
                    }
                }
            }

            return Json(new { flag = false, message = "Wrong email/password" });
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult VerifyEmail(string email)
        {
            if (_accountService.isEmailExist(email))
            {
                return Json($"Email {email} is already in use.");
            }

            return Json(true);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_accountService.Register(new RegisterDTO { Email = model.Email, Mobile = model.Mobile, Name = model.Name, Password = model.Password, Type = model.Type }))
                {
                    return Json("Registered successfully");
                }

                return View("Some error occurred");
            }

            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
