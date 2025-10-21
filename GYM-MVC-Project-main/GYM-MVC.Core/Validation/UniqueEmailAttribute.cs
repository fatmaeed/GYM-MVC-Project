using GYM.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;

namespace GYM_MVC.Core.Validation {

    public class UniqueEmailAttribute : ValidationAttribute {

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext) {
            if (value == null) return ValidationResult.Success;
            UserManager<ApplicationUser> _userManager = validationContext.GetService<UserManager<ApplicationUser>>()!;

            var result = _userManager.FindByEmailAsync(value!.ToString()!).Result;

            if (result == null)
                return ValidationResult.Success!;
            return new ValidationResult("Email already exists");
        }
    }
}