using GYM.Domain.Entities;
using GYM_MVC.Core.IRepositories;
using GYM_MVC.Data.Data;

namespace GYM_MVC.Data.Repositories {

    public class TrainerRepo : BaseRepo<Trainer>, ITrainerRepo {

        public TrainerRepo(GYMContext context) : base(context) {
        }
    }
}