using GYM.Domain.Entities;
using GYM_MVC.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace GYM_MVC.ViewModels.MemberViewModels {

    public class MemberViewModel {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string? Name { get; set; }

        [Range(12, 100)]
        public int Age { get; set; }

        [Display(Name = "Marital Status")]
        public MaritalStatus MaritalStatus { get; set; }

        [Range(30, 300)]
        public decimal Weight { get; set; }

        [Range(100, 300)]
        public decimal Height { get; set; }

        [MaxLength(200)]
        public string? Illnesses { get; set; }

        [MaxLength(200)]
        public string? Injuries { get; set; }

        [Display(Name = "Sleep Hours"), Range(0, 24)]
        public int SleepHours { get; set; }

        [Display(Name = "Available Days")]
        public string? AvailableDays { get; set; }

        public bool IsApproved { get; set; }

        [Display(Name = "Membership")]
        public int? MembershipId { get; set; }

        [Display(Name = "Trainer")]
        public int? TrainerId { get; set; }

        public string? ImagePath { get; set; }

        [Display(Name = "Profile Image")]
        public IFormFile? ImageFile { get; set; }
        public List<Membership>? Memberships { get; set; }
        public List<Trainer>? Trainers { get; set; }
    }
}