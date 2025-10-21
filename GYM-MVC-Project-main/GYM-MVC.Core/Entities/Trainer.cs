using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GYM.Domain.Entities {

    public class Trainer : BaseEntity {

        [Required, MaxLength(30)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Specialty { get; set; }

        [MaxLength(200)]
        public string Bio { get; set; }

        [Range(6000, 50000)]
        public decimal Salary { get; set; }

        public string ImagePath { get; set; }  = "/images/DefImage.jpg";

        [ForeignKey("Id")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<Member> Members { get; set; } = new List<Member>();
        public virtual ICollection<WorkoutPlan> WorkoutPlans { get; set; } = new List<WorkoutPlan>();
    }
}