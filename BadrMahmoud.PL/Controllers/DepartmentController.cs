using Demo.BLL.Interfaces;
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
            return View();
        }
    }
}
