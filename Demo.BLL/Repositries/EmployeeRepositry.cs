using Demo.BLL.Interfaces;
using Demo.DAL.Data;
using Demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositries
{
    public class EmployeeRepositry : GenaricRepositry<Employee> , IEmployeeReposititry
    {
        public EmployeeRepositry(AppDBContext appDBContext):base(appDBContext)
        {
            
        }
    }
}
