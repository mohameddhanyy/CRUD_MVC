﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Models
{
    public class Department : ModelBase
    {

        [Required(ErrorMessage ="Code is Requires !!")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Code is Requires !!")]
        public string Name { get; set; }
        [Display(Name ="Date Of Creation")]
        public DateTime DateOfCreation { get; set; }
        public ICollection<Employee> Employees {  get; set; } = new HashSet<Employee>(); 
    }
}
