using AutoMapper;
using BadrMahmoud.PL.Helpers;
using BadrMahmoud.PL.Models;
using Demo.BLL.Interfaces;
using Demo.BLL.Repositries;
using Demo.DAL.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language.Extensions;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
      
        public async Task<IActionResult> Index(string SearchInput)
        {
            IEnumerable<Employee> employees;
            var EmpRepo = _unitofWork.Repositiry<Employee>() as EmployeeRepositry;

            if (string.IsNullOrEmpty(SearchInput))
                employees = await EmpRepo.GetAll();
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
        public async Task<IActionResult> Create(EmpViewModel employeevm)
        {
            if (ModelState.IsValid)
            {
                employeevm.ImageName = await DocumentSetting.UploadFile(employeevm.Image, "Images");
                var MappedEmp = _mapper.Map<EmpViewModel, Employee>(employeevm);
                _unitofWork.Repositiry<Employee>().Add(MappedEmp);
                 await _unitofWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            return View(employeevm);
        }

        [HttpGet]

        public async Task<IActionResult> Details(int? id, string viewname = "Details")
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            var employee = await _unitofWork.Repositiry<Employee>().Get(id.Value);
            TempData["EmpId"] = id;
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
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
                id = (int)TempData["EmpId"];

            return await Details(id, "Edit");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, EmpViewModel employeevm)
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
                await _unitofWork.Complete();
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
        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(EmpViewModel employeevm)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View(employeevm);
            //}
            try
            {
                employeevm.ImageName = TempData["ImageName"] as string;
                var MappedEmp = _mapper.Map<EmpViewModel, Employee>(employeevm);
                _unitofWork.Repositiry<Employee>().Delete(MappedEmp);
                int count = await _unitofWork.Complete() ;
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
