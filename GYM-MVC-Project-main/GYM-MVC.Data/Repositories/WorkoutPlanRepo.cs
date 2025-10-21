using System.Threading.Tasks;
using GYM.Domain.Entities;
using GYM_MVC.Core.IRepositories;
using GYM_MVC.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace GYM_MVC.Data.Repositories {

    public class WorkoutPlanRepo : BaseRepo<WorkoutPlan>, IWorkoutPlanRepo {

        public WorkoutPlanRepo(GYMContext context) : base(context) {
        }

        public List<WorkoutPlan> GetWorkoutPlansByMemberId(int memberId) {
            return context.Workouts
                .Where(wp => wp.MemberId == memberId)
                .ToList();
        }
        public async Task<WorkoutPlan> GetActiveWorkOutPlan(int memberId)
        {
           return await dbSet.FirstOrDefaultAsync(wp => wp.MemberId == memberId && wp.StartDate < DateTime.Now && wp.EndDate > DateTime.Now);
        } // enddate > now && start <
    }
}