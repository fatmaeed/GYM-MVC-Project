using System.ComponentModel.DataAnnotations;

namespace GYM_MVC.ViewModels.TrainerViewModels
{

    public class CreateTrainerVM
    {

        [Required, MaxLength(30)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Specialty { get; set; }

        [MaxLength(200)]
        public string Bio { get; set; }

        [Range(6000, 50000)]
        public decimal Salary { get; set; }

        [Display(Name = "Profile Picture")]
        public IFormFile? ImageFile { get; set; }

        public string? ImagePath { get; set; }
    }
}