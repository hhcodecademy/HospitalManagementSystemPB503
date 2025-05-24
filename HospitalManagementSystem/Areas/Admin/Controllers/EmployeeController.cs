using HospitalManagementSystem.Entity;
using HospitalManagementSystem.Models;
using HospitalManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EmployeeController : Controller
    {
        private readonly IGenericService<EmployeeViewModel, Employee> _employeeService;
        private readonly IGenericService<HospitalViewModel, Hospital> _hospitalService;

        public EmployeeController(IGenericService<EmployeeViewModel, Employee> employeeService, IGenericService<HospitalViewModel, Hospital> hospitalService)
        {
            _employeeService = employeeService;
            _hospitalService = hospitalService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await _employeeService.GetAllAsync();
            return View(employees);
        }

        [HttpGet]
        public async Task< IActionResult> Create()
        {
            var hospitals = await _hospitalService.GetAllAsync();
            var hospitalvms = hospitals.Select(h => new HospitalViewModel
            {
                Id = h.Id,
                Name = h.Name
            }).ToList();
              
            var employeeViewModel = new EmployeeViewModel
            {
                Hospitals = hospitalvms
            };
            return View(employeeViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel employeeViewModel)
        {
            ModelState.Remove("id");
            if (!ModelState.IsValid)
            {
                return View(employeeViewModel);
            }
            await _employeeService.AddAsync(employeeViewModel);
            return RedirectToAction(nameof(Index));
        }


    }
}
