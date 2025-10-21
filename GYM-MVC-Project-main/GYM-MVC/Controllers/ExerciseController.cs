using System.Threading.Tasks;
using AutoMapper;
using GYM.Domain.Entities;
using GYM_MVC.Core.Helper;
using GYM_MVC.Core.IUnitOfWorks;
using GYM_MVC.ViewModels.ExerciseViewModels;
using GYM_MVC.ViewModels.WorkoutPlansViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using DayOfWeek = GYM.Domain.Entities.DayOfWeek;

namespace GYM_MVC.Controllers {
    [Authorize(Roles ="Trainer")]
    public class ExerciseController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ExerciseController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Index(int WorkoutPlanId)
        {
            var ExcerciseRepo = await _unitOfWork.ExcerciseRepo.GetExercisesByWorkoutPlanId(WorkoutPlanId);
            var workoutPlan = await _unitOfWork.WorkoutPlanRepo.GetById(WorkoutPlanId);
            ViewBag.WorkoutPlanId = WorkoutPlanId;
            ViewBag.MemberId = workoutPlan.MemberId;
            ViewBag.WorkOutPlan = _mapper.Map<DisplayWorkoutPlanVM>(workoutPlan);
            var GetAllExercises = _mapper.Map<List<EditExerciseVM>>(ExcerciseRepo);
            return View(GetAllExercises ?? new List<EditExerciseVM>());
        }

        public IActionResult Create(int WorkoutPlanId)
        {
            ViewBag.DaysOfWeek = EnumHelper.ToSelectList<DayOfWeek>();

            var model = new ExerciseVM
            {
                WorkoutPlanId = WorkoutPlanId
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ExerciseVM exerciseVM)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.DaysOfWeek = EnumHelper.ToSelectList<DayOfWeek>();
                return View(exerciseVM);
            }

            var exercise = _mapper.Map<Exercise>(exerciseVM);

            await _unitOfWork.ExcerciseRepo.Add(exercise);
            await _unitOfWork.Save();

            return RedirectToAction(nameof(Index) , new { WorkoutPlanId = exercise.WorkoutPlanId});
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.DaysOfWeek = EnumHelper.ToSelectList<DayOfWeek>();
            var exercise = await _unitOfWork.ExcerciseRepo.GetById(id);
            if (exercise == null) return NotFound();
            var exerciseVM = _mapper.Map<EditExerciseVM>(exercise);
            return View("Edit", exerciseVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditExerciseVM NewExercise)
        {
            if (!ModelState.IsValid) return View("Edit");

            var exercise = _mapper.Map<Exercise>(NewExercise);

            if (exercise == null) return NotFound();

            _unitOfWork.ExcerciseRepo.Update(exercise);
            await _unitOfWork.Save();

            return RedirectToAction(nameof(Index) , new {WorkoutPlanId = exercise.WorkoutPlanId}); 
        }

        public async Task<IActionResult> Delete(int id)
        {
            var exercise = await _unitOfWork.ExcerciseRepo.GetById(id);
            if (exercise == null) return NotFound();

            return View(DeleteConfirmed(id));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var exercise = await _unitOfWork.ExcerciseRepo.GetById(id);
            _unitOfWork.ExcerciseRepo.Delete(id);
            await _unitOfWork.Save();

            TempData["SuccessMessage"] = "Deleted successfully ";

            return RedirectToAction("Index" , new {WorkoutPlanId = exercise.WorkoutPlanId});
        }
    }
}