using System.ComponentModel.DataAnnotations;
using GYM.Domain.Entities;

namespace GYM_MVC.ViewModels.WorkoutPlansViewModels
{

    public class DisplayWorkoutPlanVM
    {

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

        public int? MemberId { get; set; }

        public string MemberName { get; set; }

        public string TrainerName { get; set; }
        public List<Exercise> Exercises { get; set; }
    }
}