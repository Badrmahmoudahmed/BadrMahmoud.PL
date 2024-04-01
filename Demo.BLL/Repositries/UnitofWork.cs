using Demo.BLL.Interfaces;
using Demo.DAL.Data;
using Demo.DAL.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositries
{
    public class UnitofWork : IUnitofWork
    {
        private readonly AppDBContext _appDBContext;
        public Hashtable _repositries { get; set; }
        //public IEmployeeReposititry EmployeeReposititry { get; set; }
        //public IDepartmentRepositries DepartmentRepositiry { get; set; }

        public UnitofWork(AppDBContext appDBContext)
        {
            //DepartmentRepositiry = new DepartmentRepositries(appDBContext);
            //EmployeeReposititry = new EmployeeRepositry(appDBContext);
            _appDBContext = appDBContext;
            _repositries = new Hashtable();
        }
        public async Task<int> Complete()
        {
            return await _appDBContext.SaveChangesAsync();
        }
        public int SaveChange()
        {
            return _appDBContext.SaveChanges();
        }

        public async ValueTask DisposeAsync()
        {
           await _appDBContext.DisposeAsync();
        }

        public IGenaricRepositiry<T> Repositiry<T>() where T : ModelBase
        {
            var key = typeof(T).Name;
            if (!_repositries.ContainsKey(key))
            {
                if(key == nameof(Employee))
                {
                    var Repo = new EmployeeRepositry(_appDBContext);
                    _repositries.Add(key, Repo);
                }else
                {
                    var Repo = new GenaricRepositry<T>(_appDBContext);
                    _repositries.Add(key, Repo);
                }
            }
            return _repositries[key] as IGenaricRepositiry<T>;
        }
    }
}
