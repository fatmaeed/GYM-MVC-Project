using System.ComponentModel.DataAnnotations;

namespace GYM_MVC.ViewModels.AccountViewModels {

    public class ResetPasswordViewModel {

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
}