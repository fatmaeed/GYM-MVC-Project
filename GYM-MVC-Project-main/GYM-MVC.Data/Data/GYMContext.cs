using GYM.Domain.Entities;
using GYM_MVC.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GYM_MVC.Data.Data {

    public class GYMContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int> {

        public GYMContext(DbContextOptions<GYMContext> options) : base(options) {
        }

        public DbSet<Member> Members { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Exercise> Exercise { get; set; }
        public DbSet<WorkoutPlan> Workouts { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Membership> Memberships { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Exercise>()
                .Property(e => e.DayOfWeek)
                .HasConversion<string>();
        }
    }
}