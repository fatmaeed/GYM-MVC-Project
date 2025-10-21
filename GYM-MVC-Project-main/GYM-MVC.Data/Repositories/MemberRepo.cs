using GYM.Domain.Entities;
using GYM_MVC.Core.IRepositories;
using GYM_MVC.Data.Data;

namespace GYM_MVC.Data.Repositories {

    public class MemberRepo : BaseRepo<Member>, IMemberRepo {

        public MemberRepo(GYMContext context) : base(context) {
        }

        public List<Member> GetMembersByTrainerId(int trainerId) {
            return context.Members
                .Where(m => m.TrainerId == trainerId && m.IsApproved && !m.IsDeleted).Distinct()
                .ToList();
        }
    }
}