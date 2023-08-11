using APIMCC.DTOs.Employees;
using APIMCC.Models;
using Client.Contracts;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

namespace Client.Controllers
{
    public class EmployeeController : Controller
    {

        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _employeeRepository.Get();
            var ListEmployee = new List<Employee>();

            if (result.Data != null)
            {
                ListEmployee = result.Data.ToList();
            }
            return View(ListEmployee);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Employee newEmploye)
        {

            var result = await _employeeRepository.Post(newEmploye);
            if (result.Status == "200")
            {
                TempData["Success"] = "Data berhasil masuk";
                return RedirectToAction(nameof(Index));
            }
            else if (result.Status == "409")
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View();
            }
            return RedirectToAction(nameof(Index));

        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid guid)
        {
            var result = await _employeeRepository.Delete(guid);
            if (result.Status == "200")
            {
                TempData["Success"] = "Data Berhasil Dihapus";
            }
            else
            {
                TempData["Error"] = "Gagal Menghapus Data";
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid guid)
        {
            var result = await _employeeRepository.Get(guid);
            return View(result);

        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid guid)
        {
            var result = await _employeeRepository.Get(guid);

            var employeeDto = new EmployeeDto
            {
                Guid = result.Data.Guid,
                NIK = result.Data.NIK,
                FirstName = result.Data.FirstName,
                LastName = result.Data.LastName,
                BirthDate = result.Data.BirthDate,
                Gender = result.Data.Gender,
                HireDate = result.Data.HireDate,
                Email = result.Data.Email,
                PhoneNumber = result.Data.PhoneNumber,
            };

            return View(employeeDto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return View(employee);
            }

            var result = await _employeeRepository.Put(employee.Guid, employee);

            if (result.Status == "200")
            {
                TempData["Success"] = "Successfully updated";
            }

            else
            {
                TempData["Error"] = "Failed to Updated";
            }

            return RedirectToAction(nameof(Index));
        }



    }
}