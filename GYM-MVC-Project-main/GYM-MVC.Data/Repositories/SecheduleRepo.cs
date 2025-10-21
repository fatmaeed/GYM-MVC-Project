using GYM_MVC.Core.Entities;
using GYM_MVC.Core.IRepositories;
using GYM_MVC.Data.Data;

namespace GYM_MVC.Data.Repositories {

    public class SecheduleRepo : BaseRepo<Schedule>, IScheduleRepo {

        public SecheduleRepo(GYMContext gYMContext) : base(gYMContext) {
        }
    }
}