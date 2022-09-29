using Microsoft.AspNetCore.Mvc;
using SoftWizECommerce.CustomValidation;
using SoftWizHelpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SoftWizECommerce.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required"), DataType(DataType.EmailAddress), EmailAddress]
        [Remote(action: "VerifyEmail", controller: "Account")]
        public string Email { get; set; }
        public string Mobile { get; set; }
        [Required(ErrorMessage = "Password  is required"), DataType(DataType.Password), Compare(nameof(ConfirmPassword))]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm Password  is required"), DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        public Enums.UserType Type { get; set; }

        [CheckBoxRequired(ErrorMessage = "You need to be agreed to terms in order to register")]
        public bool IAgree { get; set; }
    }
}
