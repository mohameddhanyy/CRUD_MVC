using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Models
{
    public class Employee : ModelBase
    {

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

        public DateTime HireDate { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreationDate { get; set; }

        public int? DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
