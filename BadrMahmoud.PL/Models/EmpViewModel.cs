using Demo.DAL.Models;
using System.ComponentModel.DataAnnotations;
using System;

namespace BadrMahmoud.PL.Models
{
    public class EmpViewModel
    {
        public int Id { get; set; }

        [Required (ErrorMessage ="Name is Req Yabny !!")]
        public string Name { get; set; }

        [Range(20, 30)]
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

        public int? DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
