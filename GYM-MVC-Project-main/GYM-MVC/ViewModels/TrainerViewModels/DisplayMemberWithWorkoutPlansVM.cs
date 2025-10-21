using GYM.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace GYM_MVC.ViewModels.TrainerViewModels {

    public class DisplayMemberWithWorkoutPlansVM {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        [Range(12, 70)]
        public int Age { get; set; }

        public string MaritalStatus { get; set; }

        [Range(30, 300)]
        public decimal Weight { get; set; }

        [Range(100, 250)]
        public decimal Height { get; set; }

        [MaxLength(200)]
        public string Illnesses { get; set; }

        [MaxLength(200)]
        public string Injuries { get; set; }

        [Range(0, 24)]
        public int SleepHours { get; set; }

        public string AvailableDays { get; set; }
        public string ImagePath { get; set; }
        public bool IsApproved { get; set; } = false;

        public virtual ICollection<WorkoutPlan> WorkoutPlan { get; set; }
    }
}