using AutoMapper;
using BadrMahmoud.PL.Helpers;
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
        private readonly IUnitofWork _unitofWork;

        //private readonly IEmployeeReposititry _employeeReposititry;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public EmployeeController(/*IEmployeeReposititry employeeReposititry ,*/IUnitofWork unitofWork,IMapper mapper ,IWebHostEnvironment env)
        {
            //_employeeReposititry = employeeReposititry;
            _unitofWork = unitofWork;
            _mapper = mapper;
            _env = env;
        }
      
        public IActionResult Index(string SearchInput)
        {
            IEnumerable<Employee> employees;
            var EmpRepo = _unitofWork.Repositiry<Employee>() as EmployeeRepositry;

            if (string.IsNullOrEmpty(SearchInput))
                employees = EmpRepo.GetAll();
            else
                employees = EmpRepo.SearchByName(SearchInput.ToLower());

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
                employeevm.ImageName = DocumentSetting.UploadFile(employeevm.Image, "Images");
                var MappedEmp = _mapper.Map<EmpViewModel, Employee>(employeevm);
                _unitofWork.Repositiry<Employee>().Add(MappedEmp);
                _unitofWork.Complete();
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
            var employee = _unitofWork.Repositiry<Employee>().Get(id.Value);

            if (employee is null)
            {
                return NotFound();
            }
            var MappedEmp = _mapper.Map<Employee, EmpViewModel>(employee);
            if (viewname.Equals("Delete", StringComparison.OrdinalIgnoreCase))
                TempData["ImageName"] = employee.ImageName;
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
                _unitofWork.Repositiry<Employee>().Update(MappedEmp);
                _unitofWork.Complete();
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
                employeevm.ImageName = TempData["ImageName"] as string;
                var MappedEmp = _mapper.Map<EmpViewModel, Employee>(employeevm);
                _unitofWork.Repositiry<Employee>().Delete(MappedEmp);
                int count = _unitofWork.Complete();
                if (count > 0) 
                {
                    DocumentSetting.DeleteFile(employeevm.ImageName, "Images");
                }
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
