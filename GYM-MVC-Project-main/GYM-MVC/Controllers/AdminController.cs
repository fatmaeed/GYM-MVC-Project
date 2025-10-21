using AutoMapper;
using GYM.Domain.Entities;
using GYM_MVC.Core.IRepositories;
using GYM_MVC.Core.IUnitOfWorks;
using GYM_MVC.Data.UnitOfWorks;
using GYM_MVC.ViewModels.ScheduleViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GYM_MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller

    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public AdminController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Dashboard()
        {
            var schedules = await Task.Run(() => unitOfWork.ScheduleRepo.GetAll().ToList());
            var scheduleViewModels = mapper.Map<List<ScheduleViewModel>>(schedules);

            var scheduleTable = scheduleViewModels
                .GroupBy(s => s.Time.ToString(@"hh\:mm"))
                .Select(g => new
                {
                    Time = g.Key,
                    Monday = string.Join(", ", g.Where(s => s.DayOfWeek.Equals("Monday", StringComparison.OrdinalIgnoreCase)).Select(s => s.ClassName)),
                    Tuesday = string.Join(", ", g.Where(s => s.DayOfWeek.Equals("Tuesday", StringComparison.OrdinalIgnoreCase)).Select(s => s.ClassName)),
                    Wednesday = string.Join(", ", g.Where(s => s.DayOfWeek.Equals("Wednesday", StringComparison.OrdinalIgnoreCase)).Select(s => s.ClassName)),
                    Thursday = string.Join(", ", g.Where(s => s.DayOfWeek.Equals("Thursday", StringComparison.OrdinalIgnoreCase)).Select(s => s.ClassName)),
                    Friday = string.Join(", ", g.Where(s => s.DayOfWeek.Equals("Friday", StringComparison.OrdinalIgnoreCase)).Select(s => s.ClassName)),
                    Saturday = string.Join(", ", g.Where(s => s.DayOfWeek.Equals("Saturday", StringComparison.OrdinalIgnoreCase)).Select(s => s.ClassName)),
                    Sunday = string.Join(", ", g.Where(s => s.DayOfWeek.Equals("Sunday", StringComparison.OrdinalIgnoreCase)).Select(s => s.ClassName))
                })
                .OrderBy(s => s.Time)
                .ToList();

            ViewBag.ScheduleTable = scheduleTable;
            return View();
        }


    }
}