using GYM.Domain.Entities;

namespace GYM_MVC.Core.IRepositories {

    public interface IMemberRepo : IBaseRepo<Member> {

        List<Member> GetMembersByTrainerId(int trainerId);
    }
}