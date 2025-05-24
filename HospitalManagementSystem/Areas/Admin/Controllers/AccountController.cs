using HospitalManagementSystem.Entity;
using HospitalManagementSystem.Models;
using HospitalManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IGenericService<EmployeeViewModel, Employee> _employeeService;
        private readonly IEmailService _emailService;


        public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IGenericService<EmployeeViewModel, Employee> employeeService, IEmailService emailService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _employeeService = employeeService;
            _emailService = emailService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("Email", "Invalid login attempt.");
                return View(model);
            }
            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("Password", "Invalid login attempt.");
                return View(model);
            }
            return RedirectToAction("Index", "Home");

        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = new AppUser
            {
                UserName = model.UserName,
                Email = model.Email,
                Location = model.Location,
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("Password", error.Description);
                }
                return View(model);
            }
            var addedUser = await _userManager.FindByEmailAsync(model.Email);

            await _employeeService.AddAsync(new EmployeeViewModel
            {
                FirstName = model.Name,
                LastName = model.LastName,
                Email = model.Email,
                UserId = addedUser.Id,
                HospitalId=1,
                DateOfJoining=DateTime.UtcNow
            });
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, token = token }, Request.Scheme);
            await _emailService.SendEmailAsync(user.Email, "Confirm your email", $"Please confirm your account by clicking this link: <a href='{confirmationLink}'>link</a>");
            TempData["Email"] = model.Email;
            return RedirectToAction("Info","Account");
        }

        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {

            var user = await _userManager.FindByIdAsync(userId);
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Email confirmation failed");

            }
            return View("Error");
        }
        public IActionResult Info()
        {
            string email = TempData["Email"] as string;
            ViewBag.Email = email;
            return View();
        }
    }
}
