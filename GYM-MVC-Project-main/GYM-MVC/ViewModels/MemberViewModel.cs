namespace GYM_MVC.ViewModels {

    public class MemberViewModel {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int Age { get; set; }
        public string MaritalStatus { get; set; }
        public decimal? Weight { get; set; }
        public decimal? Height { get; set; }
        public string Illnesses { get; set; }
        public string Injuries { get; set; }
        public int? SleepHours { get; set; }
        public string AvailableDays { get; set; }
        public TrainerViewModel Trainer { get; set; }
        public WorkoutPlanViewModel WorkoutPlan { get; set; }
    }
}