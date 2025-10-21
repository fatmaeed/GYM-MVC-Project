using GYM.Domain.Entities;

namespace GYM_MVC.Core.Entities {

    public class Schedule : BaseEntity {
        public string DayOfWeek { get; set; }

        public TimeOnly Time { get; set; }

        public string ClassName { get; set; }
    }
}