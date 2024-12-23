using Demo.BLL.Interfaces;
using Demo.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepo;

        public DepartmentController(IDepartmentRepository departmentRepo)
        {
            _departmentRepo = departmentRepo;
        }

        public IActionResult Index()
        {
            var departments = _departmentRepo.GetAll();
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
                var count = _departmentRepo.Add(department);
                if (count > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(department);
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (!id.HasValue) return BadRequest();

            var department = _departmentRepo.Get(id.Value);

            if (department == null) return NotFound();

            return View(department);
        }
    }
}
