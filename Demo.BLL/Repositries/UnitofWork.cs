using Demo.BLL.Interfaces;
using Demo.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositries
{
    public class UnitofWork : IUnitofWork
    {
        private readonly AppDBContext _appDBContext;

        public IEmployeeReposititry EmployeeReposititry { get; set; }
        public IDepartmentRepositries DepartmentRepositiry { get; set; }

        public UnitofWork(AppDBContext appDBContext)
        {
            DepartmentRepositiry = new DepartmentRepositries(appDBContext);
            EmployeeReposititry = new EmployeeRepositry(appDBContext);
            _appDBContext = appDBContext;
        }
        public void Complete()
        {
            _appDBContext.SaveChanges();
        }
        public int SaveChange()
        {
            return _appDBContext.SaveChanges();
        }

        public void Dispose()
        {
            _appDBContext.Dispose();
        }
    }
}
