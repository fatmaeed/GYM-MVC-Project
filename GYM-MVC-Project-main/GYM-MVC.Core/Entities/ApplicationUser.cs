using Microsoft.AspNetCore.Identity;

namespace GYM.Domain.Entities {

    public class ApplicationUser : IdentityUser<int> {
        public virtual Member Member { get; set; }

        public virtual Trainer Trainer { get; set; }
    }
}