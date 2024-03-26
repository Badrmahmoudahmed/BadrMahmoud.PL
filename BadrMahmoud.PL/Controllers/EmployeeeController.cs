using AutoMapper;
using BadrMahmoud.PL.Models;
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
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public EmployeeController(IEmployeeReposititry employeeReposititry ,IMapper mapper ,IWebHostEnvironment env)
        {
            _employeeReposititry = employeeReposititry;
            _mapper = mapper;
            _env = env;
        }
      
        public IActionResult Index(string SearchInput)
        {
            IEnumerable<Employee> employees;


            if (string.IsNullOrEmpty(SearchInput))
                employees = _employeeReposititry.GetAll();
            else
                employees = _employeeReposititry.SearchByName(SearchInput.ToLower());

            var MappedEmp = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmpViewModel>>(employees);
            return View(MappedEmp);
        }

        [HttpGet]
        public IActionResult Create()
        {
            
            return View();
        }
        [HttpPost]
        public IActionResult Create(EmpViewModel employeevm)
        {
            if (ModelState.IsValid)
            {
                var MappedEmp = _mapper.Map<EmpViewModel, Employee>(employeevm);
                _employeeReposititry.Add(MappedEmp);
                return RedirectToAction(nameof(Index));
            }
            return View(employeevm);
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
            var MappedEmp = _mapper.Map<Employee, EmpViewModel>(employee);

            return View(viewname, MappedEmp);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            return Details(id, "Edit");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, EmpViewModel employeevm)
        {
            if (id != employeevm.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View(employeevm);
            }

            try
            {
                var MappedEmp = _mapper.Map<EmpViewModel, Employee>(employeevm);
                _employeeReposititry.Update(MappedEmp);
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
                return View(employeevm);
            }
        }
        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }
        [HttpPost]
        public IActionResult Delete(EmpViewModel employeevm)
        {
            if (!ModelState.IsValid)
            {
                return View(employeevm);
            }
            try
            {
                var MappedEmp = _mapper.Map<EmpViewModel, Employee>(employeevm);
                _employeeReposititry.Delete(MappedEmp);

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
                return View(employeevm);
            }
        }
    }
}
