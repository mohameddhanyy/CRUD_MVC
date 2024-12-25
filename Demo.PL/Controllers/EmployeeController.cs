using Demo.BLL.Interfaces;
using Demo.DAL.Models;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using AutoMapper;
using System.Collections.Generic;
namespace Demo.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IEmployeeRepository _employeeRepo;

        public EmployeeController(IMapper mapper, IEmployeeRepository employeeRepo)
        {
            _mapper = mapper;
            _employeeRepo = employeeRepo;
        }

        public IActionResult Index(string name)
        {
            var employees = Enumerable.Empty<Employee>();
            if (String.IsNullOrEmpty(name))
                employees = _employeeRepo.GetAll();
            else
                employees = _employeeRepo.GetEmployeeByName(name.ToLower());
            var mappedEmployees = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);

            return View(mappedEmployees);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmployeeViewModel employeeVM)
        {
            //MANUAL MAPPING

            ///var emp = new Employee()
            ///{
            ///    Name = employeeVM.Name,
            ///    Salary = employeeVM.Salary,
            ///    Address = employeeVM.Address,
            ///    Email = employeeVM.Email,
            ///    IsActive = employeeVM.IsActive,
            ///    PhoneNumber = employeeVM.PhoneNumber,
            ///    HireDate = employeeVM.HireDate,            
            ///};

            var mappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
            if (ModelState.IsValid)
            {
                var count = _employeeRepo.Add(mappedEmployee);
                if (count > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(employeeVM);
        }

        [HttpGet]
        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (!id.HasValue) return BadRequest();

            var employee = _employeeRepo.Get(id.Value);

            var mappedEmployee = _mapper.Map<Employee, EmployeeViewModel>(employee);

            if (employee == null) return NotFound();

            return View(viewName, mappedEmployee);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, EmployeeViewModel employeeVM)
        {
            if (id != employeeVM.Id) return BadRequest();
            var mappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);

            if (ModelState.IsValid)
            {
                try
                {
                    var count = _employeeRepo.Update(mappedEmployee);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(String.Empty, ex.Message);
                }
            }
            return View(employeeVM);

        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id, EmployeeViewModel employeeVM)
        {
            if (id != employeeVM.Id) return BadRequest();
            var mappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
            if (ModelState.IsValid)
            {
                try
                {

                    var count = _employeeRepo.Delete(mappedEmployee);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(String.Empty, ex.Message);
                }
            }
            return View(employeeVM);
        }

    }
}
