using Demo.BLL.Interfaces;
using Demo.BLL.Repositries;
using Demo.DAL.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BadrMahmoud.PL.Controllers
{
    public class EmployeeController : Controller
    {

        private readonly IEmployeeReposititry _employeeReposititry;
        private readonly IWebHostEnvironment _env;

        public EmployeeController(IEmployeeReposititry employeeReposititry ,IWebHostEnvironment env)
        {
            _employeeReposititry = employeeReposititry;
            _env = env;
        }
      
        public IActionResult Index(string SearchInput)
        {
            var employees = Enumerable.Empty<Employee>();


            if (string.IsNullOrEmpty(SearchInput))
                employees = _employeeReposititry.GetAll();
            else
                employees = _employeeReposititry.SearchByName(SearchInput.ToLower());
            
            return View(employees);
        }

        [HttpGet]
        public IActionResult Create()
        {
            
            return View();
        }
        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _employeeReposititry.Add(employee);
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        [HttpGet]

        public IActionResult Details(int? id, string viewname = "Details")
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            var employee = _employeeReposititry.Get(id.Value);

            if (employee is null)
            {
                return NotFound();
            }

            return View(viewname, employee);
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
            if (id != employee.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View(employee);
            }

            try
            {
                _employeeReposititry.Update(employee);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "There is an Error");
                }
                return View(employee);
            }
        }
        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }
        [HttpPost]
        public IActionResult Delete(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return View(employee);
            }
            try
            {
                _employeeReposititry.Delete(employee);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "There is an Error");
                }
                return View(employee);
            }
        }
    }
}
