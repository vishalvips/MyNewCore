using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewCoreWebApp.Models
{
    public class DepartmentModel
    {
        [Required]
        public int DepartmentId { get; set; }
        [Required]
        [Display(Name ="Department Name")]
        public string DepartmentName { get; set; }
    }
}
