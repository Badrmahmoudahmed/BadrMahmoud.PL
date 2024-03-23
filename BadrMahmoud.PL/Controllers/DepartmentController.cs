using Demo.BLL.Interfaces;
using Demo.BLL.Repositries;
using Demo.DAL.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;

namespace BadrMahmoud.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepositries _departmentRepositries;
        private readonly IWebHostEnvironment _env;

        public DepartmentController(IDepartmentRepositries departmentRepositries , IWebHostEnvironment env)
        {
            _departmentRepositries = departmentRepositries;
            _env = env;
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

        [HttpGet]

        public IActionResult Details(int? id , string viewname = "Details")
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            var department = _departmentRepositries.Get(id.Value);

            if (department is null) 
            {
                return NotFound();
            }

            return View(viewname,department);
        }

        [HttpGet]
        public IActionResult Edit(int? id) 
        {
            return Details(id, "Edit");

        }

        [HttpPost]
        public IActionResult Edit([FromRoute]int id,Department department)
        {
            if (id != department.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid) 
            {
                return View(department);
            }

            try
            {
                _departmentRepositries.Update(department);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                if(_env.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "There is an Error");
                }
                return View(department);
            }
        }
    }
}
