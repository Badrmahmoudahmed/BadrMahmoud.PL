﻿using Demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Interfaces
{
    public interface IUnitofWork : IDisposable
    {
        //public IEmployeeReposititry EmployeeReposititry { get; set; }
        //public IDepartmentRepositries DepartmentRepositiry { get; set; }

        IGenaricRepositiry<T> Repositiry<T>() where T : ModelBase;
        int Complete();
        int SaveChange();
    }
}
