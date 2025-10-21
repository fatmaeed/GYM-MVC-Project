using GYM.Domain.Entities;

namespace GYM_MVC.Core.IRepositories {

    public interface IWorkoutPlanRepo : IBaseRepo<WorkoutPlan> {

        List<WorkoutPlan> GetWorkoutPlansByMemberId(int memberId);
        public Task<WorkoutPlan> GetActiveWorkOutPlan(int memberId);
    }
}