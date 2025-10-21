using GYM.Domain.Entities;

namespace GYM_MVC.Core.Entities {

    //public enum MembershipType {
    //    Basic,
    //    Premium,
    //    VIP
    //}

    //1	 Basic	30	100   "One-month membership"

    public class Membership : BaseEntity {
        public string Type { get; set; }
        public decimal Price { get; set; }
        public int DurationInDays { get; set; }

        public string Description { get; set; }
        public virtual ICollection<Member> Members { get; set; } = new List<Member>();
    }
}