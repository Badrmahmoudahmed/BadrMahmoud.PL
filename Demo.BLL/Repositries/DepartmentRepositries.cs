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
    internal class DepartmentRepositries : IDepartmentRepositries
    {
        private readonly AppDBContext _dbcontext;

        public DepartmentRepositries(AppDBContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public int Add(Department department)
        {
            _dbcontext.Departments.Add(department);
            return _dbcontext.SaveChanges();
        }

        public int Delete(Department department)
        {
            _dbcontext.Departments.Remove(department);
            return _dbcontext.SaveChanges();
        }

        public Department Get(int id)
        {
            return _dbcontext.Departments.Find(id);
        }

        public IEnumerable<Department> GetAll()
        {
           return _dbcontext.Departments.AsNoTracking().ToList();
        }

        public int Update(Department department)
        {
            _dbcontext.Departments.Update(department);
            return _dbcontext.SaveChanges();
        }
    }
}
