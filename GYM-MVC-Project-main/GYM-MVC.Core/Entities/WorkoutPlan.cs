using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GYM.Domain.Entities {

    public class WorkoutPlan : BaseEntity {

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

        [ForeignKey("MemberId")]
        public virtual Member Member { get; set; }

        public int? TrainerId { get; set; }

        [ForeignKey("TrainerId")]
        public virtual Trainer Trainer { get; set; }

        public virtual ICollection<Exercise> Exercises { get; set; } = new List<Exercise>();
    }
}