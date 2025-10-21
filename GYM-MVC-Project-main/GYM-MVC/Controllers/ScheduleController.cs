using AutoMapper;
using GYM_MVC.Core.Entities;
using GYM_MVC.Core.IUnitOfWorks;
using GYM_MVC.ViewModels.ScheduleViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GYM_MVC.Controllers
{

    [Authorize(Roles ="Admin")]
    public class ScheduleController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

    public    ScheduleController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ScheduleViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            await unitOfWork.ScheduleRepo.Add(mapper.Map<Schedule>(model));
            await unitOfWork.Save();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var schedules = await Task.Run(() => unitOfWork.ScheduleRepo.GetAll().ToList());
            var scheduleViewModels = mapper.Map<List<ScheduleViewModel>>(schedules);
            return View(scheduleViewModels);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var schedule = await unitOfWork.ScheduleRepo.GetById(id);
            if (schedule == null) return NotFound();

            return View(mapper.Map<UpdateScheduleViewModel>(schedule));
        }


        [HttpPost]
        public async Task<IActionResult> Edit(UpdateScheduleViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            unitOfWork.ScheduleRepo.Update(mapper.Map<Schedule>(model)); 
            await unitOfWork.Save();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var schedule = await unitOfWork.ScheduleRepo.GetById(id);
            if (schedule == null) return NotFound();
            unitOfWork.ScheduleRepo.Delete(schedule.Id);
            await unitOfWork.Save();
            return RedirectToAction("Index");
        }
    }
}
