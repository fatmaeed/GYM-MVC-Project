using GYM_MVC.Core.IRepositories;
using GYM_MVC.Core.IUnitOfWorks;
using GYM_MVC.Data.Data;
using GYM_MVC.Data.Repositories;

namespace GYM_MVC.Data.UnitOfWorks {

    public class UnitOfWork : IUnitOfWork {
        private GYMContext context;
        private IMemberRepo memberRepo;
        private ITrainerRepo trainerRepo;
        private IExcerciseRepo excerciseRepo;
        private IWorkoutPlanRepo workoutPlanRepo;
        private IScheduleRepo scheduleRepo;
        private IMembershipRepo membershipRepo;

        public UnitOfWork(GYMContext context) {
            this.context = context;
        }

        public async Task<int> Save() => await context.SaveChangesAsync();

        public IMemberRepo MemberRepo {
            get {
                if (memberRepo is null)
                    memberRepo = new MemberRepo(context);
                return memberRepo;
            }
        }

        public ITrainerRepo TrainerRepo {
            get {
                if (trainerRepo is null)
                    trainerRepo = new TrainerRepo(context);
                return trainerRepo;
            }
        }

        public IExcerciseRepo ExcerciseRepo {
            get {
                if (excerciseRepo is null)
                    excerciseRepo = new ExcerciseRepo(context);
                return excerciseRepo;
            }
        }

        public IWorkoutPlanRepo WorkoutPlanRepo {
            get {
                if (workoutPlanRepo is null)
                    workoutPlanRepo = new WorkoutPlanRepo(context);
                return workoutPlanRepo;
            }
        }

        public IScheduleRepo ScheduleRepo {
            get {
                if (scheduleRepo is null)
                    scheduleRepo = new SecheduleRepo(context);
                return scheduleRepo;
            }
        }

        public IMembershipRepo MembershipRepo {
            get {
                if (membershipRepo is null)
                    membershipRepo = new MembershipRepo(context);
                return membershipRepo;
            }
        }
    }
}