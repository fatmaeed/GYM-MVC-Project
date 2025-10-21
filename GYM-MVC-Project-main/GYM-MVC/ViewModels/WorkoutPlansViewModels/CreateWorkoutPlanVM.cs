using System.ComponentModel.DataAnnotations;

namespace GYM_MVC.ViewModels.WorkoutPlansViewModels {

    public class CreateWorkoutPlanVM {

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required, MaxLength(500)]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [MaxLength(200)]
        public string GeneralInfo { get; set; }

        [MaxLength(200)]
        public string InjuryInfo { get; set; }

        [Display(Name = "Member")]
        public int? MemberId { get; set; }

        public int? TrainerId { get; set; }
    }
}