using System.Security.Claims;
using AutoMapper;
using AutoMapper.Execution;
using GYM.Domain.Entities;
using GYM_MVC.Core.IUnitOfWorks;
using GYM_MVC.ViewModels.WorkoutPlansViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GYM_MVC.Controllers {

    [Authorize(Roles = "Trainer")]
    public class WorkoutPlanController : Controller {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public WorkoutPlanController(IUnitOfWork unitOfWork, IMapper mapper) {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public IActionResult GetAllWorkoutPlans() {
            var workoutPlans = unitOfWork.WorkoutPlanRepo.GetAll().ToList();

            return View(mapper.Map<List<DisplayWorkoutPlanVM>>(workoutPlans));
        }

        public IActionResult Create(int memberId) {
            var allMembers = unitOfWork.MemberRepo.GetAll().ToList();
            ViewBag.MemberId = memberId;

            ViewBag.MembersList = new SelectList(allMembers, "Id", "Name", memberId);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateWorkoutPlanVM createWorkoutPlan) {
            if (createWorkoutPlan is null)
                return NotFound();
            createWorkoutPlan.TrainerId =  int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (ModelState.IsValid) {
                var workOutPlan = mapper.Map<WorkoutPlan>(createWorkoutPlan);
                await unitOfWork.WorkoutPlanRepo.Add(workOutPlan);
                await unitOfWork.Save();
                return RedirectToAction("Index", "Exercise", new { WorkoutPlanId = workOutPlan.Id });
                // return RedirectToAction("GetMemberWithWorkoutPlans", "Trainer",new {Id = createWorkoutPlan.MemberId});
            }
            var allMembers = unitOfWork.MemberRepo.GetAll().ToList();
            ViewBag.MemberId = createWorkoutPlan.MemberId;
            ViewBag.MembersList = new SelectList(allMembers, "Id", "Name", createWorkoutPlan.MemberId);
            return View(createWorkoutPlan);
        }

        public async Task<IActionResult> Edit(int? id) {
            if (id is null || !await unitOfWork.WorkoutPlanRepo.Contains(wp => wp.Id == id))
                return NotFound();
            var workOutPlan = unitOfWork.WorkoutPlanRepo.GetById(id.Value);
            return View(mapper.Map<EditWorkoutPlanVM>(workOutPlan.Result));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int? id, EditWorkoutPlanVM editWorkoutPlanVM) {
            if (id is null || editWorkoutPlanVM.Id != id)
                return NotFound();

            if (ModelState.IsValid) {
                unitOfWork.WorkoutPlanRepo.Update(mapper.Map<WorkoutPlan>(editWorkoutPlanVM));
                unitOfWork.Save();
                return RedirectToAction("GetMemberWithWorkoutPlans", "Trainer", new { Id = editWorkoutPlanVM.MemberId });
            }
            return View(editWorkoutPlanVM);
        }

        public async Task<IActionResult> Delete(int? id) {
            if (id is null || !await unitOfWork.WorkoutPlanRepo.Contains(wp => wp.Id == id))
                return NotFound();

            var workoutPlan = await unitOfWork.WorkoutPlanRepo.GetById(id.Value);
            return View(mapper.Map<DisplayWorkoutPlanVM>(workoutPlan));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id) {
            if (id is null || !await unitOfWork.WorkoutPlanRepo.Contains(wp => wp.Id == id))
                return NotFound();
            int? memberId = unitOfWork.WorkoutPlanRepo.GetById(id.Value).Result.MemberId;
            unitOfWork.WorkoutPlanRepo.Delete(id.Value);
            await unitOfWork.Save();
            return RedirectToAction("GetMemberWithWorkoutPlans", "Trainer", new { Id = memberId });
        }
    }
}