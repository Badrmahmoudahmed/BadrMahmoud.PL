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

        [Range(20 , 30)]
        public int? Age { get; set; }

        public string Adress { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNum { get; set; }

        [Display(Name = "Hiring Date")]
        public DateTime HiringDate { get; set; }

        public Gender Gender { get; set; }
    }
}
