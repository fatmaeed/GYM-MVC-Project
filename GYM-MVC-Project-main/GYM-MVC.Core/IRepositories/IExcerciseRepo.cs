using GYM.Domain.Entities;

namespace GYM_MVC.Core.IRepositories {
    public interface IExcerciseRepo : IBaseRepo<Exercise> {
        public Task<List<Exercise>> GetExercisesByWorkoutPlanId(int workoutPlanId);
    }
}