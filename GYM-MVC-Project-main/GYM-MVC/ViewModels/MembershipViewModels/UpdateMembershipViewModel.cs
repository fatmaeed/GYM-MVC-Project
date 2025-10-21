using Microsoft.AspNetCore.Mvc.Rendering;

namespace GYM_MVC.ViewModels.MembershipViewModels {

    public class UpdateMembershipViewModel : MembershipViewModel {
        public int Id { get; set; }

        public SelectList? MembershipTypeList { get; set; }
    }
}