using Microsoft.AspNetCore.Mvc.Rendering;

namespace GYM_MVC.ViewModels.MembershipViewModels {

    public class CreateMembershipViewModel : MembershipViewModel {
        public string SelectedMembershipType { get; set; }
        public SelectList? MembershipTypeList { get; set; }
    }
}