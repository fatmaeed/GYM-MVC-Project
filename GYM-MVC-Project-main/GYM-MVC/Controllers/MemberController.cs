using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using GYM.Domain.Entities;
using GYM_MVC.Core.IUnitOfWorks;
using GYM_MVC.ViewModels.AccountViewModels;
using GYM_MVC.ViewModels.MemberViewModels;
using GYM_MVC.ViewModels.WorkoutPlansViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GYM_MVC.Controllers {
    public class MemberController : Controller {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper mapper;

        public MemberController(IUnitOfWork unitOfWork, IWebHostEnvironment env, IMapper mapper) {
            _unitOfWork = unitOfWork;
            _env = env;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index() {
            var members = _unitOfWork.MemberRepo.GetAll()
                .Select(MapToViewModel)
                .ToList();
            return View(members);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id) {
            var member = await _unitOfWork.MemberRepo.GetById(id);
            if (member == null) return NotFound();

            var vm = MapToViewModel(member);
            vm.Trainers = _unitOfWork.TrainerRepo.GetAll().ToList();
            vm.Memberships = _unitOfWork.MembershipRepo.GetAll().ToList();
            return View(vm);
        }

        [Authorize(Roles ="Admin,Member")]
        [HttpGet]
        public IActionResult Create() {
            RegisterMemberFromAdmin vm = new RegisterMemberFromAdmin();

            return View(vm);
        }

        [Authorize(Roles = "Admin,Member")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MemberViewModel vm) {
            if (!ModelState.IsValid) return View(vm);

            var member = new Member();
            MapToEntity(vm, member);

            if (vm.ImageFile != null && vm.ImageFile.Length > 0)
                member.ImagePath = await SaveImageAsync(vm.ImageFile);

            await _unitOfWork.MemberRepo.Add(member);
            await _unitOfWork.Save(); // Save changes to DB

            return RedirectToAction(nameof(Index));
        }
        //[HttpGet]
        //public async Task<IActionResult> Edit(int id)
        //{
        //    var member = await _unitOfWork.MemberRepo.GetById(id);
        //    if (member == null) return NotFound();

        //    var vm = MapToViewModel(member);
        //    return View(vm); 
        //}

        [Authorize(Roles = "Admin,Member")]

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MemberViewModel vm) {
            if (!ModelState.IsValid)
            {
                vm.Trainers = _unitOfWork.TrainerRepo.GetAll().ToList();
                vm.Memberships = _unitOfWork.MembershipRepo.GetAll().ToList();

                return View("Details", vm);
            }
            var member = await _unitOfWork.MemberRepo.GetById(vm.Id);
            if (member == null) return NotFound();

            MapToEntity(vm, member);

            if (vm.ImageFile != null && vm.ImageFile.Length > 0) {
                if (!string.IsNullOrWhiteSpace(member.ImagePath)) {
                    var oldPath = Path.Combine(_env.WebRootPath, member.ImagePath.TrimStart('/'));
                    if (System.IO.File.Exists(oldPath)) System.IO.File.Delete(oldPath);
                }

                member.ImagePath = await SaveImageAsync(vm.ImageFile);
            }

            _unitOfWork.MemberRepo.Update(member);
            await _unitOfWork.Save(); // Save changes to DB

            if (User.IsInRole("Admin"))
                return RedirectToAction(nameof(Index));
            return RedirectToAction("ActiveWorkoutPlan", "Member", new { id = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value });
        }
        [Authorize(Roles = "Admin,Member")]

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id) {
            _unitOfWork.MemberRepo.Delete(id);
            await _unitOfWork.Save(); // Save changes to DB
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Admin,Member")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRange(List<int> memberIds) {
            var members = _unitOfWork.MemberRepo
                          .GetAll()
                          .Where(m => memberIds.Contains(m.Id))
                          .ToList();

            _unitOfWork.MemberRepo.DeleteRange(members);
            await _unitOfWork.Save(); // Save changes to DB

            return RedirectToAction(nameof(Index));
        }

        private static MemberViewModel MapToViewModel(Member m) => new() {
            Id = m.Id,
            Name = m.Name,
            Age = m.Age,
            MaritalStatus = m.MaritalStatus,
            Weight = m.Weight,
            Height = m.Height,
            Illnesses = m.Illnesses,
            Injuries = m.Injuries,
            SleepHours = m.SleepHours,
            AvailableDays = m.AvailableDays,
            ImagePath = m.ImagePath,
            IsApproved = m.IsApproved,
            MembershipId = m.MembershipId,
            TrainerId = m.TrainerId
        };
        public int getMemberCount()
        {
            return _unitOfWork.MemberRepo.Count().Result;
        }
        private static void MapToEntity(MemberViewModel vm, Member m) {
            m.Name = vm.Name;
            m.Age = vm.Age;
            m.MaritalStatus = vm.MaritalStatus;
            m.Weight = vm.Weight;
            m.Height = vm.Height;
            m.Illnesses = vm.Illnesses;
            m.Injuries = vm.Injuries;
            m.SleepHours = vm.SleepHours;
            m.AvailableDays = vm.AvailableDays;
            m.IsApproved = vm.IsApproved;
            m.MembershipId = vm.MembershipId;
            m.TrainerId = vm.TrainerId;
        }

        private async Task<string> SaveImageAsync(Microsoft.AspNetCore.Http.IFormFile file) {
            var folder = Path.Combine(_env.WebRootPath, "images", "members");
            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var path = Path.Combine(folder, fileName);

            await using var stream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(stream);

            return $"/images/members/{fileName}";
        }

        [Authorize(Roles = "Member")]

        public async Task<IActionResult> ActiveWorkOutPlan(int id)
        {
            var activeWorkoutPlan = await _unitOfWork.WorkoutPlanRepo.GetActiveWorkOutPlan(id);
            var member = await _unitOfWork.MemberRepo.GetById(id);
            ViewBag.IsApproved = member.IsApproved;
            return View(mapper.Map<DisplayWorkoutPlanVM>(activeWorkoutPlan));

        }
        [Authorize(Roles = "Member")]
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var userName = User.Identity?.Name;
            if (string.IsNullOrWhiteSpace(userName)) return RedirectToAction("Login", "Account");

            var member = _unitOfWork.MemberRepo.GetAll()
                            .FirstOrDefault(m => m.Name == userName);

            if (member == null) return NotFound("Member profile not found.");

            var vm = MapToViewModel(member);
            return View(vm);
        }
    }
   
}