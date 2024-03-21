using Demo.BLL.Interfaces;
using Demo.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace BadrMahmoud.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepositries _departmentRepositries;

        public DepartmentController(IDepartmentRepositries departmentRepositries)
        {
            _departmentRepositries = departmentRepositries;
        }
        public IActionResult Index()
        {
            var Department = _departmentRepositries.GetAll();
            return View(Department);
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
                _departmentRepositries.Add(department);
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }
    }
}
