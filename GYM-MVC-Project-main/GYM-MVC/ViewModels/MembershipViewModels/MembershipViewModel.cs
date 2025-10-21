using System.ComponentModel.DataAnnotations;

namespace GYM_MVC.ViewModels.MembershipViewModels {

    public class MembershipViewModel {
        public string SelectedMembershipType { get; set; }

        [Range(10, 20000)]
        public decimal Price { get; set; }

        [Display(Name = "Number Of Days"), Range(1, 365)]
        public int DurationInDays { get; set; }

        [Display(Name = "Description of Membership")]
        public string? Description { get; set; }
    }
}