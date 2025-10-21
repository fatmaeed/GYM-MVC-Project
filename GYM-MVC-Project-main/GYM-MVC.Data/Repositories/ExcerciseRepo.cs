using GYM.Domain.Entities;
using GYM_MVC.Core.IRepositories;
using GYM_MVC.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace GYM_MVC.Data.Repositories {

    public class ExcerciseRepo : BaseRepo<Exercise>, IExcerciseRepo {

        public ExcerciseRepo(GYMContext context) : base(context) {
        }

        public async Task<List<Exercise>> GetExercisesByWorkoutPlanId(int workoutPlanId) {
            return await dbSet.Where(e => e.WorkoutPlanId == workoutPlanId && !e.IsDeleted).ToListAsync();
        }



    }
}