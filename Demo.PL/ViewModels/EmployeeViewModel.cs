using Demo.DAL.Models;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Http;

namespace Demo.PL.ViewModels
{
    public class EmployeeViewModel
    {
        public  int  Id { get; set; }

        [Required(ErrorMessage = "Must Have a Value")]
        [MaxLength(20, ErrorMessage = "Max Linght is 20")]
        [MinLength(5, ErrorMessage = "Min Linght is 5")]
        public string Name { get; set; }

        [RegularExpression(@"^[1-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$",
            ErrorMessage = "Address must be Like this -> 123-Street-City-Country")]
        public string Address { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        [Display(Name = "Active")]

        public bool IsActive { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Phone Number")]

        public string PhoneNumber { get; set; }
        [Display(Name = "Hire Date")]

        public IFormFile Image { get; set; }
        public string ImageName { get; set; }

        public DateTime HireDate { get; set; }
        public int? DepartmentId { get; set; }
        public virtual Department Department { get; set; }

    }
}
