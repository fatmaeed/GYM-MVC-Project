using AutoMapper;
using GYM.Domain.Entities;
using GYM_MVC.Core.Entities;
using GYM_MVC.ViewModels;
using GYM_MVC.ViewModels.AccountViewModels;
using GYM_MVC.ViewModels.ExerciseViewModels;
using GYM_MVC.ViewModels.MembershipViewModels;
using GYM_MVC.ViewModels.ScheduleViewModels;
using GYM_MVC.ViewModels.TrainerViewModels;
using GYM_MVC.ViewModels.WorkoutPlansViewModels;

namespace GYM_MVC.Core.MapperConf {

    public class MapperConfig : Profile {

        public MapperConfig() {
            CreateMap<RegisterUserViewModel, ApplicationUser>().AfterMap((src, dist) => {
                dist.PasswordHash = src.Password;
                dist.UserName = src.Name;
                dist.Email = src.Email;
                dist.PhoneNumber = src.PhoneNumber;
            });

            CreateMap<RegisterMemberViewModel, Member>().AfterMap((src, dist) => {
                dist.Name = src.MemberName;
                dist.Age = DateTime.Today.Year - src.BirthDate.Year;
                dist.AvailableDays = src.AvailableDays.ToString();
                dist.Illnesses = src.Illnesses ?? "";
                dist.Injuries = src.Injuries ?? "";
                dist.TrainerId = src.SelectedTrainerId;
                dist.MembershipId = src.SelectedMembershipId;
            });
            CreateMap<RegisterMemberFromAdmin, Member>().IncludeBase<RegisterMemberViewModel, Member>()
             .AfterMap((src, dist) => {
                 dist.IsApproved = src.IsApproved;
             });
            CreateMap<RegisterTrainerViewModel, Trainer>().ReverseMap();
            CreateMap<Member, RegisterMemberViewModel>().AfterMap((src, dist) => {
            });
            CreateMap<LoginUserViewModel, ApplicationUser>().AfterMap((src, dist) => {
                dist.UserName = src.UserName;
            });

            CreateMap<CreateScheduleViewModel, Schedule>().ReverseMap();
            CreateMap<Schedule, UpdateScheduleViewModel>().ReverseMap();

            CreateMap<Schedule, ScheduleViewModel>().ReverseMap();
            CreateMap<Member, MemberViewModel>().ReverseMap();
            CreateMap<Trainer, DisplayTrainerVM>().ReverseMap();
            CreateMap<Trainer, CreateTrainerVM>().ReverseMap();
            CreateMap<Trainer, EditTrainerVM>().ReverseMap();
            CreateMap<Member, MemberByTrainerIdVM>().ReverseMap();
            CreateMap<Member, DisplayMemberWithWorkoutPlansVM>().ReverseMap();
            CreateMap<WorkoutPlan, DisplayWorkoutPlanVM>().AfterMap((src, dist) => {
                dist.MemberName = src.Member.Name;
                dist.TrainerName = src.Trainer.Name;
                dist.Exercises = src.Exercises.ToList();
            }).ReverseMap();
            CreateMap<WorkoutPlan, CreateWorkoutPlanVM>().ReverseMap();
            CreateMap<WorkoutPlan, EditWorkoutPlanVM>().ReverseMap();

            CreateMap<CreateMembershipViewModel, Membership>().AfterMap(
                (src, dist) =>
                    dist.Type = src.SelectedMembershipType
                );
            CreateMap<UpdateMembershipViewModel, Membership>().AfterMap(
                (src, dist) => {
                    dist.Type = src.SelectedMembershipType;
                    dist.Id = src.Id;
                }).ReverseMap().AfterMap(
                   (src, dist) => {
                       dist.SelectedMembershipType = src.Type.ToString();
                   }
                );
            CreateMap<Membership, DisplayMembershipViewModel>().AfterMap(
                (src, dist) => {
                    dist.SelectedMembershipType = src.Type.ToString();
                }
                );
            CreateMap<Exercise, EditExerciseVM>().AfterMap((src, dest) => {
                dest.MemberId = src.WorkoutPlan.MemberId;
            }).ReverseMap();
            CreateMap<Exercise, ExerciseVM>().ReverseMap();
        }
    }
}