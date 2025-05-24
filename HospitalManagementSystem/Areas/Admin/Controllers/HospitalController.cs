using HospitalManagementSystem.Entity;
using HospitalManagementSystem.Models;
using HospitalManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HospitalController : Controller
    {
        private readonly IGenericService<HospitalViewModel, Hospital> _hospitalService;

        public HospitalController(IGenericService<HospitalViewModel, Hospital> hospitalService)
        {
            _hospitalService = hospitalService;
        }

        public async  Task<IActionResult> Index()
        {
            var hospitals = await _hospitalService.GetAllAsync();

            return View(hospitals);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(HospitalViewModel hospitalViewModel)
        {
            ModelState.Remove("id");
            if (!ModelState.IsValid)
            {
                return View(hospitalViewModel);
            }
            await _hospitalService.AddAsync(hospitalViewModel);
            return RedirectToAction(nameof(Index));
        }

    }
}
