using AutoMapper;
using Demo.DAL.Models;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Demo.PL.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public RoleController(RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var mappedRole = _mapper.Map<RoleViewModel,IdentityRole>(model);
                await _roleManager.CreateAsync(mappedRole);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public async Task<IActionResult> Index(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                var roles = await _roleManager.Roles.Select(R => new RoleViewModel()
                {
                    Id = R.Id,
                    RoleName = R.Name,
                }).ToListAsync();
                return View(roles);
            }
            else
            {
                var role = await _roleManager.FindByNameAsync(name);
                var mappedRole = new RoleViewModel()
                {
                    Id = role.Id,
                    RoleName = role.Name,
                };
                return View(new List<RoleViewModel>() { mappedRole });
            }
        }

        public async Task<IActionResult> Details(string id, string viewName = "Details")
        {
            if (string.IsNullOrEmpty(id)) return BadRequest();

            var user = await _roleManager.FindByIdAsync(id);

            var mappedEmployee = _mapper.Map<IdentityRole, RoleViewModel>(user);

            if (user == null) return NotFound();

            return View(viewName, mappedEmployee);
        }

        [HttpGet]
        public Task<IActionResult> Edit(string id)
        {
            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id, RoleViewModel UpdatedRole)
        {

            if (id != UpdatedRole.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _roleManager.FindByIdAsync(id);
                    user.Name = UpdatedRole.RoleName;

                    await _roleManager.UpdateAsync(user);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(String.Empty, ex.Message);
                }
            }
            return View(UpdatedRole);

        }




        [HttpGet]
        public Task<IActionResult> Delete(string id)
        {
            return Details(id, "Delete");
        }

        [HttpPost(Name = "Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(string id)
        {
            {
                try
                {
                    var user = await _roleManager.FindByIdAsync(id);
                    await _roleManager.DeleteAsync(user);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(String.Empty, ex.Message);
                    return RedirectToAction("Error", "Home");
                }
            }
        }

    }
}
