using Demo.DAL.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace BadrMahmoud.PL.Models
{
    public class DeptViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Code Req !!")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Name Req !!")]
        public string Name { get; set; }
        [Display(Name = "Date Of Creation")]
        public DateTime DateofCreation { get; set; }

        public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    }
}
