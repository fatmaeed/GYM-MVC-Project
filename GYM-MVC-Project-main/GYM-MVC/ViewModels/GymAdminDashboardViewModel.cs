namespace GYM_MVC.ViewModels {

    public class GymAdminDashboardViewModel {
        public int TotalMembers { get; set; }
        public int TotalTrainers { get; set; }
        public int TotalWorkoutPlans { get; set; }
        public List<ActivityLog> RecentActivities { get; set; }
        public List<MemberViewModel> Members { get; set; }
        public List<TrainerViewModel> Trainers { get; set; }
        public List<WorkoutPlanViewModel> WorkoutPlans { get; set; }
        public List<MembershipViewModel> Memberships { get; set; }
    }
}