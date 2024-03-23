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
        public int Add(T entity)
        {
            _appDBContext.Add(entity);
            return _appDBContext.SaveChanges();
        }

        public int Delete(T entity)
        {
            _appDBContext?.Remove(entity);
            return _appDBContext.SaveChanges();
        }

        public T Get(int id)
        {
            return _appDBContext.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _appDBContext.Set<T>().AsNoTracking().ToList();
        }

        public int Update(T entity)
        {
             _appDBContext.Update(entity);
            return _appDBContext.SaveChanges();
        }
    }
}
