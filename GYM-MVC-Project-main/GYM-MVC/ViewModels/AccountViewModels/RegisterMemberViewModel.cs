using GYM.Domain.Entities;
using GYM_MVC.Core.Helper;
using GYM_MVC.ViewModels.MembershipViewModels;
using GYM_MVC.ViewModels.TrainerViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GYM_MVC.ViewModels.AccountViewModels
{

    public class RegisterMemberViewModel : RegisterUserViewModel
    {

        [Display(Name = "Your Name"), Required]
        public string? MemberName { get; set; }

        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Marital Status")]
        public MaritalStatus MaterialStatus { get; set; }

        [Range(30, 300)]
        public decimal Weight { get; set; }

        [Range(100, 250)]
        public decimal Height { get; set; }

        [MaxLength(200)]
        public string? Illnesses { get; set; }

        [MaxLength(200)]
        public string? Injuries { get; set; }

        [Range(0, 24), Display(Name = "Sleep Hours")]
        public int SleepHours { get; set; }

        [Display(Name = "Available Days")]
        public int AvailableDays { get; set; }

        public SelectList? MaterialStatusList
        {
            get
            {
                return EnumHelper.ToSelectList<MaritalStatus>();
            }
        }

        public IFormFile? Image { get; set; }
        public List<string> materialStatuseLists = new List<string>() { "Single", "Married" };

        [Display(Name = "Trainer")]
        public int? SelectedTrainerId { get; set; }

        public List<DisplayTrainerVM> AvailableTrainers { get; set; } = new();

        [Display(Name = "Membership")]
        public int? SelectedMembershipId { get; set; }

        public List<DisplayMembershipViewModel> AvailableMemberships { get; set; } = new();
    }
}