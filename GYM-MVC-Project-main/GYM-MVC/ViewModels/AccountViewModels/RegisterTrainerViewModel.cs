using System.ComponentModel.DataAnnotations;

namespace GYM_MVC.ViewModels.AccountViewModels {

    public class RegisterTrainerViewModel : RegisterUserViewModel {

        [Required, MaxLength(30), Display(Name = "Full Name")]
        public string TrainerName { get; set; }

        [MaxLength(100)]
        public string Specialty { get; set; }

        [MaxLength(200)]
        public string Bio { get; set; }

        [Range(6000, 50000)]
        public decimal Salary { get; set; }

        public IFormFile? Image { get; set; }
    }
}