using GYM_MVC.Core.Entities;
using GYM_MVC.Core.IRepositories;
using GYM_MVC.Data.Data;

namespace GYM_MVC.Data.Repositories {

    internal class MembershipRepo : BaseRepo<Membership>, IMembershipRepo {

        public MembershipRepo(GYMContext _context) : base(_context) {
        }
    }
}