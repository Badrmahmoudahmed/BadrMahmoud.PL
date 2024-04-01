using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Models
{
    public enum Gender
    {
        Male = 1, Female = 2
    }
    public class Employee : ModelBase
    {

        public string Name { get; set; }

        public int? Age { get; set; }

        public string Adress { get; set; }

        public string Email { get; set; }

        public decimal Salary { get; set; }


        public string PhoneNum { get; set; }

        public DateTime HiringDate { get; set; }

        public Gender Gender { get; set; }

        public int? DepartmentId { get; set; }
        public Department Department { get; set; }
        public string ImageName { get; set; }
    }
}
