using AutoMapper;
using GYM.Domain.Entities;
using GYM_MVC.Core.Helper;
using GYM_MVC.Core.IUnitOfWorks;
using GYM_MVC.ViewModels.AccountViewModels;
using GYM_MVC.ViewModels.MembershipViewModels;
using GYM_MVC.ViewModels.TrainerViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GYM_MVC.Controllers {

    public class AccountController : Controller {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> signInManager;
        private IMapper mapper;
        private IEmailSender emailSender;
        private readonly IUnitOfWork unitOfWork;

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IMapper mapper,
            IEmailSender emailSender,
            IUnitOfWork unitOfWork
            ) {
            this._userManager = userManager;
            this.signInManager = signInManager;
            this.mapper = mapper;
            this.emailSender = emailSender;
            this.unitOfWork = unitOfWork;
        }

        
        [HttpGet]
        public IActionResult Login() {   
            if(!User.Identity.IsAuthenticated)
                 return View("Login");
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginTheUser(LoginUserViewModel loginUserViewModel) {
            if (ModelState.IsValid) {
                ApplicationUser? user = await _userManager.FindByNameAsync(loginUserViewModel.UserName);
                if (user != null && user.EmailConfirmed) {
                    bool result = await _userManager.CheckPasswordAsync(user, loginUserViewModel.Password);
                    if (result) {
                        await signInManager.SignInAsync(user, loginUserViewModel.RememberMe);
                        switch (User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)!.Value) {
                            case "Member":
                                return RedirectToAction("ActiveWorkOutPlan", "Member", new { Id = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value });

                            case "Trainer":
                                return RedirectToAction("GetMembersByTrainerId", "Trainer", new { Id = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value });

                            case "Admin":
                                return RedirectToAction("Dashboard", "Admin");
                        }
                    }
                }
            }
            ModelState.AddModelError(string.Empty, "Invalid User");
            return View("Login", loginUserViewModel);
        }

        
        [HttpGet]
        public IActionResult Register() {
            if (User.Identity.IsAuthenticated)
                return NotFound();
            var model = new RegisterMemberViewModel {
                AvailableTrainers = unitOfWork.TrainerRepo.GetAll()
              .Select(t => mapper.Map<DisplayTrainerVM>(t)).ToList(),

                AvailableMemberships = unitOfWork.MembershipRepo.GetAll()
              .Select(m => mapper.Map<DisplayMembershipViewModel>(m)).ToList()
            };
            return View("Register", model);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult RegisterTrainer() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterTheMember(RegisterMemberViewModel registerMemberViewModel) {
            if (ModelState.IsValid) {
                ApplicationUser user = mapper.Map<RegisterMemberViewModel, ApplicationUser>(registerMemberViewModel);
                Member member = mapper.Map<RegisterMemberViewModel, Member>(registerMemberViewModel);
                IdentityResult result = _userManager.CreateAsync(user, registerMemberViewModel.Password).Result;
                var resultOfRole = await _userManager.AddToRoleAsync(user, "Member");

                if (registerMemberViewModel.Image != null) {
                    UploadImageStatus status = await ImageHandler.UploadImage(registerMemberViewModel.Image, "members");
                    if (status is UploadImageError) {
                        ModelState.AddModelError(string.Empty, status.Message);
                        return View("Register", registerMemberViewModel);
                    }
                    member.ImagePath = ((UploadImageSuccess)status).FileName;
                }

                if (result.Succeeded && resultOfRole.Succeeded) {
                    member.Id = user.Id;
                    await unitOfWork.MemberRepo.Add(member);
                    await unitOfWork.Save();
                    await SendEmailConfirmation(user);
                    return View("confirmEmail");
                } else {
                    foreach (var error in result.Errors) {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            registerMemberViewModel.AvailableTrainers = unitOfWork.TrainerRepo.GetAll()
             .Select(t => mapper.Map<DisplayTrainerVM>(t)).ToList();

            registerMemberViewModel.AvailableMemberships = unitOfWork.MembershipRepo.GetAll()
         .Select(m => mapper.Map<DisplayMembershipViewModel>(m)).ToList();

            return View("Register", registerMemberViewModel);
        }

        [Authorize(Roles ="Admin")]
        public IActionResult RegisterTheMemberFromAdmin() {
            RegisterMemberFromAdmin registerMemFormAdmin = new RegisterMemberFromAdmin();
            registerMemFormAdmin.AvailableMemberships = mapper.Map<List<DisplayMembershipViewModel>>(unitOfWork.MembershipRepo.GetAll().ToList());
            registerMemFormAdmin.AvailableTrainers = mapper.Map<List<DisplayTrainerVM>>(unitOfWork.TrainerRepo.GetAll().ToList());
            return View(registerMemFormAdmin);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterTheMemberFromAdmin(RegisterMemberFromAdmin registerMemFormAdmin) {
            if (ModelState.IsValid) {
                ApplicationUser user = mapper.Map<RegisterMemberViewModel, ApplicationUser>(registerMemFormAdmin);
                Member member = mapper.Map<RegisterMemberFromAdmin, Member>(registerMemFormAdmin);
                IdentityResult result = _userManager.CreateAsync(user, registerMemFormAdmin.Password).Result;
                var resultOfRole = await _userManager.AddToRoleAsync(user, "Member");

                if (registerMemFormAdmin.Image != null) {
                    UploadImageStatus status = await ImageHandler.UploadImage(registerMemFormAdmin.Image, "members");
                    if (status is UploadImageError) {
                        ModelState.AddModelError(string.Empty, status.Message);
                        return View("Register", registerMemFormAdmin);
                    }
                    member.ImagePath = ((UploadImageSuccess)status).FileName;
                }


                if (result.Succeeded && resultOfRole.Succeeded) {
                    member.Id = user.Id;
                    await unitOfWork.MemberRepo.Add(member);
                    await unitOfWork.Save();
                    await SendEmailConfirmation(user);
                    return View("confirmEmail");
                } else {
                    foreach (var error in result.Errors) {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            registerMemFormAdmin.AvailableMemberships = mapper.Map<List<DisplayMembershipViewModel>>(unitOfWork.MembershipRepo.GetAll().ToList());
            registerMemFormAdmin.AvailableTrainers = mapper.Map<List<DisplayTrainerVM>>(unitOfWork.TrainerRepo.GetAll().ToList());
            return View(registerMemFormAdmin);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult RegisterTheTrainer ()
        {
            return View("RegisterTrainer");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterTheTrainer(RegisterTrainerViewModel registerTrainerViewModel) {
            if (ModelState.IsValid) {
                ApplicationUser user = mapper.Map<RegisterTrainerViewModel, ApplicationUser>(registerTrainerViewModel);
                Trainer trainer = mapper.Map<RegisterTrainerViewModel, Trainer>(registerTrainerViewModel);

                if (registerTrainerViewModel.Image != null) {
                    UploadImageStatus status = await ImageHandler.UploadImage(registerTrainerViewModel.Image, "Trainer");
                    if (status is UploadImageError) {
                        ModelState.AddModelError(string.Empty, status.Message);
                        return View("RegisterTrainer", registerTrainerViewModel);
                    }
                    trainer.ImagePath = ((UploadImageSuccess)status).FileName;
                }
                //else
                //{
                //    trainer.ImagePath = "~/images/DefImage.jpg";
                //}

                    IdentityResult result = _userManager.CreateAsync(user, registerTrainerViewModel.Password).Result;
                var resultOfRole = await _userManager.AddToRoleAsync(user, "Trainer");
                if (result.Succeeded && resultOfRole.Succeeded) {
                    trainer.Id = user.Id;
                    await unitOfWork.TrainerRepo.Add(trainer);
                    await unitOfWork.Save();
                    await SendEmailConfirmation(user);
                    return View("confirmEmail");
                } else {
                    foreach (var error in result.Errors) {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View("RegisterTrainer", registerTrainerViewModel);
        }

        public async Task<IActionResult> ConfirmEmail(int userId, string code) {
            ApplicationUser? user = _userManager.FindByIdAsync(userId.ToString()).Result;
            if (user == null) {
                return NotFound();
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded) {
                await signInManager.SignInAsync(user, false);
                return RedirectToAction("Index", "Home");
            }
            return View("Error");
        }

        [Authorize]
        public async Task<IActionResult> Logout() {
            await signInManager.SignOutAsync();
            return View("Login");
        }

        [HttpGet]
        public IActionResult ForgetPassword() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgotPasswordViewModel forgotPasswordViewModel) {
            if (ModelState.IsValid) {
                ApplicationUser? user = await _userManager.FindByEmailAsync(forgotPasswordViewModel.Email);
                if (user != null) {
                    string code = await _userManager.GeneratePasswordResetTokenAsync(user);
                    string callbackUrl = Url.Action("ResetPassword", "Account", new { email = user.Email, code = code }, Request.Scheme)!;
                    await emailSender.SendEmailAsync(forgotPasswordViewModel.Email, "Reset your password", "Please reset your password by clicking this link: <a href=\"" + callbackUrl + "\">Reset</a>");
                }
            }
            return View("confirmEmail");
        }

        [HttpGet]
        public IActionResult ResetPassword(string code, string email) {
            if (code == null || email == null)
                return RedirectToAction("Index", "Home");

            return View(new ResetPasswordViewModel { Code = code, Email = email });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model) {
            if (!ModelState.IsValid) {
                ModelState.AddModelError(string.Empty, ModelState.Values.First().Errors.First().ErrorMessage);

                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null) {
                return NotFound();
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded) {
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors) {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }

        //--------------------------
        private async Task SendEmailConfirmation(ApplicationUser user) {    
            Task<string> code = _userManager.GenerateEmailConfirmationTokenAsync(user);
            string callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code.Result }, Request.Scheme)!;
            await emailSender.SendEmailAsync(user.Email!, "Confirm your account", "Please confirm your account by clicking this link: <a href=\"" + callbackUrl + "\">Confirm</a>");
        }
    }
}