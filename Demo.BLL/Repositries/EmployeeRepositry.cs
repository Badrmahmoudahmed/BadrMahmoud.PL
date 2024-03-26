using Demo.BLL.Interfaces;
using Demo.DAL.Data;
using Demo.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositries
{
    public class EmployeeRepositry : GenaricRepositry<Employee> , IEmployeeReposititry
    {
        private readonly AppDBContext _appDBContext;

        public EmployeeRepositry(AppDBContext appDBContext) : base(appDBContext)
        {
            _appDBContext = appDBContext;
        }
        public IQueryable<Employee> SearchByName(string name)
         => _appDBContext.Employees.Where(e => e.Name.ToLower().Contains(name));
       
    }
}
