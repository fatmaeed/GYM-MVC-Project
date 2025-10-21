using AutoMapper;
using GYM_MVC.Core.Entities;
using GYM_MVC.Core.Helper;
using GYM_MVC.Core.IUnitOfWorks;
using GYM_MVC.ViewModels.MembershipViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GYM_MVC.Controllers {

    [Authorize(Roles ="Admin")]
    public class MembershipController : Controller {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public MembershipController(IUnitOfWork unitOfWork, IMapper mapper) {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult Create() {
            CreateMembershipViewModel model = new CreateMembershipViewModel();
           // model.MembershipTypeList = EnumHelper.ToSelectList<MembershipType>();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateMembershipViewModel model) {
            //model.MembershipTypeList = EnumHelper.ToSelectList<MembershipType>();
            if (!ModelState.IsValid) return View(model);
            await unitOfWork.MembershipRepo.Add(mapper.Map<Membership>(model));
            await unitOfWork.Save();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id) {
            Membership membership = await unitOfWork.MembershipRepo.GetById(id);
            UpdateMembershipViewModel model = mapper.Map<UpdateMembershipViewModel>(membership);
            //model.MembershipTypeList = EnumHelper.ToSelectList<MembershipType>();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateMembershipViewModel model) {
           // model.MembershipTypeList = EnumHelper.ToSelectList<MembershipType>();
            if (!ModelState.IsValid) return View(model);
            unitOfWork.MembershipRepo.Update(mapper.Map<Membership>(model));
            await unitOfWork.Save();
            return RedirectToAction("index");
        }

        [HttpGet]
        public IActionResult Index() {
            return View(mapper.Map<List<DisplayMembershipViewModel>>(unitOfWork.MembershipRepo.GetAll()));
        }

        public async Task<IActionResult> Delete(int id) {
            var membership = await unitOfWork.MembershipRepo.GetById(id);
            if (membership == null)
                return NotFound();

            return View(mapper.Map<DisplayMembershipViewModel>(membership));
        }

        public async Task<IActionResult> DeleteConfirmed(int id) {
            unitOfWork.MembershipRepo.Delete(id);
            await unitOfWork.Save();
            return RedirectToAction("index");
        }

        public async Task<IActionResult> DeleteRange(List<DisplayMembershipViewModel> models) {
            unitOfWork.MembershipRepo.DeleteRange(mapper.Map<List<Membership>>(models));
            await unitOfWork.Save();
            return RedirectToAction("index");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id) {
            Membership membership = await unitOfWork.MembershipRepo.GetById(id);
            return View(mapper.Map<DisplayMembershipViewModel>(membership));
        }
    }
}

// test push 