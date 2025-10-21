using GYM_MVC.Core.Validation;
using System.ComponentModel.DataAnnotations;

namespace GYM_MVC.ViewModels.AccountViewModels {

    public class RegisterUserViewModel {

        [UniqueUserName]
        [Display(Name = "User Name")]
        public string Name { get; set; }

        [UniqueEmail]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Email is not valid")]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match")]
        public string ConfirmPassword { get; set; }
    }
}