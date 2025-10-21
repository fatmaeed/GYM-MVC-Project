using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GYM.Domain.Entities {

    public enum DayOfWeek {
        Sunday,
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday
    }

    public class Exercise : BaseEntity {

        [Required, MaxLength(50)]
        public string Name { get; set; }

        [Range(1, 100)]
        public int Repetitions { get; set; }

        [Range(1, 10)]
        public int Sets { get; set; }

        [Range(10, 300)]
        public int RestTime { get; set; }

        public DayOfWeek DayOfWeek { get; set; }

        [ForeignKey("WorkoutPlan")]
        public int? WorkoutPlanId { get; set; }

        public virtual WorkoutPlan WorkoutPlan { get; set; }
    }
}