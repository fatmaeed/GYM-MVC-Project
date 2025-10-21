using System.ComponentModel.DataAnnotations;

namespace GYM_MVC.ViewModels.ExerciseViewModels {

    public class ExerciseVM {

        [Required, MaxLength(50)]
        public string Name { get; set; }

        [Range(1, 100)]
        public int Repetitions { get; set; }

        [Range(1, 10)]
        public int Sets { get; set; }

        [Range(10, 300)]
        public int RestTime { get; set; }

        public DayOfWeek DayOfWeek { get; set; }

        public int? WorkoutPlanId { get; set; }
    }
}
