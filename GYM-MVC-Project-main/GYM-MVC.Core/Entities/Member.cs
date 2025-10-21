using GYM_MVC.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GYM.Domain.Entities {

    public enum MaritalStatus {
        Single,
        Married
    }

    public class Member : BaseEntity {

        [Required, MaxLength(50)]
        public string Name { get; set; }

        [Range(12, 70)]
        public int Age { get; set; }

        public MaritalStatus MaritalStatus { get; set; }

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
        public string ImagePath { get; set; } = "/images/DefImage.jpg";
        public bool IsApproved { get; set; } = false;

        public int? MembershipId { get; set; }

        [ForeignKey("MembershipId")]
        public virtual Membership Membership { get; set; }

        [ForeignKey("Id")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        public int? TrainerId { get; set; }

        [ForeignKey("TrainerId")]
        public virtual Trainer Trainer { get; set; }

        public virtual ICollection<WorkoutPlan> WorkoutPlan { get; set; } = new List<WorkoutPlan>();
    }
}