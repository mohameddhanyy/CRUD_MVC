using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Models
{
    internal class Department
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Code is Requires !!")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Code is Requires !!")]
        public string Name { get; set; }
        public DateTime DateOfCreation { get; set; }
    }
}
