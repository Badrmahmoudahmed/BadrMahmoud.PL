using AutoMapper;
using BadrMahmoud.PL.Models;
using Demo.BLL.Interfaces;
using Demo.BLL.Repositries;
using Demo.DAL.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using System;
using System.Collections.Generic;

namespace BadrMahmoud.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IUnitofWork _unitofWork;

        //private readonly IDepartmentRepositries _departmentRepositries;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public DepartmentController(/*IDepartmentRepositries departmentRepositries*/IUnitofWork unitofWork,IMapper _Mapper, IWebHostEnvironment env)
        {
            //_departmentRepositries = departmentRepositries;
            _unitofWork = unitofWork;
            _mapper = _Mapper;
            _env = env;
        }
        public IActionResult Index()
        {
            var Department = _unitofWork.Repositiry<Department>().GetAll();
            ViewData[nameof(Message)] = "Hello ViewDate";
            ViewBag.Message = "hello ViewBag";
            var MappedDept = _mapper.Map<IEnumerable<Department>, IEnumerable<DeptViewModel>>(Department);
            return View(MappedDept);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(DeptViewModel departmentvm)
        {
            if (ModelState.IsValid)
            {
                var MappedDept = _mapper.Map<DeptViewModel,Department>(departmentvm);
                _unitofWork.Repositiry<Department>().Add(MappedDept);
                int Count = _unitofWork.SaveChange();
                if (Count > 0)
                {
                    TempData["Message"] = "Department Created Successfully";

                }
                else
                {
                    TempData["Message"] = "An Error Occuerd !";
                }
                return RedirectToAction(nameof(Index));
            }
            return View(departmentvm);
        }

        [HttpGet]

        public IActionResult Details(int? id, string viewname = "Details")
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            var department = _unitofWork.Repositiry<Department>().Get(id.Value);

            if (department is null)
            {
                return NotFound();
            }
            var MappedDept = _mapper.Map<Department ,DeptViewModel>(department);
            return View(viewname, MappedDept);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            return Details(id, "Edit");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, DeptViewModel departmentvm)
        {
            if (id != departmentvm.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View(departmentvm);
            }

            try
            {
                var MappedDept = _mapper.Map<DeptViewModel, Department>(departmentvm);
                _unitofWork.Repositiry<Department>().Update(MappedDept);
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
                return View(departmentvm);
            }
        }
        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }
        [HttpPost]
        public IActionResult Delete(DeptViewModel departmentvm)
        {
            if (!ModelState.IsValid)
            {
                return View(departmentvm);
            }
            try
            {
                var MappedDept = _mapper.Map<DeptViewModel, Department>(departmentvm);
                _unitofWork.Repositiry<Department>().Delete(MappedDept);
                _unitofWork.Complete();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                if(_env.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }else
                {
                    ModelState.AddModelError(string.Empty, "There is an Error");
                }
                return View(departmentvm);
            }
        }
    }
}
