namespace GYM_MVC.ViewModels {

    public class WorkoutPlanViewModel {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string MemberName { get; set; }
        public string TrainerName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ExercisesCount { get; set; }
    }
}