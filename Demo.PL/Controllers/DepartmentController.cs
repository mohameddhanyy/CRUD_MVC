﻿using Demo.BLL.Interfaces;
using Demo.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Demo.PL.Controllers
{
    [Authorize]

    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            ViewData["Message"] = "Hello";
            ViewBag.Message = "Hello This is Message : From ViewBag";
            var departments = _unitOfWork.DepartmentRepository.GetAll();
            return View(departments);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
                var count = _unitOfWork.DepartmentRepository.Add(department);
                _unitOfWork.Complete();
                if (count > 0)
                    TempData["Message"] = "Department Created Successfully";
                else
                    TempData["Message"] = "an Error Occured While Create Department";
                return RedirectToAction("Index");
            }
            return View(department);
        }

        [HttpGet]
        public IActionResult Details(int? id, string viewName = "Details")
        {
            if (!id.HasValue) return BadRequest();

            var department = _unitOfWork.DepartmentRepository.Get(id.Value);

            if (department == null) return NotFound();

            return View(viewName, department);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            return Details(id, "Edit");
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int id, Department department)
        {
            if (id != department.Id) return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var count = _unitOfWork.DepartmentRepository.Update(department);
                    _unitOfWork.Complete();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(String.Empty, ex.Message);
                }
            }
            return View(department);

        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        public IActionResult Delete([FromRoute] int id, Department department)
        {
            if (id != department.Id) return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var count = _unitOfWork.DepartmentRepository.Delete(department);
                    _unitOfWork.Complete();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(String.Empty, ex.Message);
                }
            }
            return View(department);
        }
    }
}
