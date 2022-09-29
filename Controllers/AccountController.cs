using Microsoft.AspNetCore.Mvc;
using SoftWizBusinessLogic.DTOs;
using SoftWizBusinessLogic.Interfaces;
using SoftWizDataAccess.Models;
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
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        
        public IActionResult Register()
        {
            return View();
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
    }
}
