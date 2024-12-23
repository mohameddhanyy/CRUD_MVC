using Demo.BLL.Interfaces;
using Demo.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Demo.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepo;

        public EmployeeController(IEmployeeRepository employeeRepo)
        {
            _employeeRepo = employeeRepo;
        }

        public IActionResult Index()
        {
            var employees = _employeeRepo.GetAll();
            return View(employees);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                var count = _employeeRepo.Add(employee);
                if (count > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(employee);
        }

        [HttpGet]
        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (!id.HasValue) return BadRequest();

            var employee = _employeeRepo.Get(id.Value);

            if (employee == null) return NotFound();

            return View(viewName, employee);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, Employee employee)
        {
            if (id != employee.Id) return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var count = _employeeRepo.Update(employee);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(String.Empty, ex.Message);
                }
            }
            return View(employee);

        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id, Employee employee)
        {
            if (id != employee.Id) return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var count = _employeeRepo.Delete(employee);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(String.Empty, ex.Message);
                }
            }
            return View(employee);
        }

    }
}
