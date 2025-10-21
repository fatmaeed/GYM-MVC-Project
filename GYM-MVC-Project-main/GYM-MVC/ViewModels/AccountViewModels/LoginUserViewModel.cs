using System.ComponentModel.DataAnnotations;

namespace GYM_MVC.ViewModels.AccountViewModels {

    public class LoginUserViewModel {

        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}