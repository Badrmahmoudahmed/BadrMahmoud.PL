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
    public class GenaricRepositry<T> :  IGenaricRepositiry<T> where T : ModelBase
    {
        private protected readonly AppDBContext _appDBContext;

        public GenaricRepositry(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }
        public void Add(T entity)
        {
            _appDBContext.Add(entity);
 
        }

        public void Delete(T entity)
        {
            _appDBContext?.Remove(entity);

        }

        public async Task<T> Get(int id)
        {
            return await _appDBContext.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            if (typeof(T) == typeof(Employee))
            {
                return  (IEnumerable<T>) await _appDBContext.Employees.Include(E => E.Department).AsNoTracking().ToListAsync();
            }
            return await _appDBContext.Set<T>().AsNoTracking().ToListAsync();
        }

        public void Update(T entity)
        {
             _appDBContext.Update(entity);
        }
    }
}
