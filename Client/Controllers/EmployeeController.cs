﻿using APIMCC.DTOs.Employees;
using APIMCC.Models;
using Client.Contracts;
using Microsoft.AspNetCore.Mvc;

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
    }
}