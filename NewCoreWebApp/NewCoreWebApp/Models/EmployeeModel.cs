using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewCoreWebApp.Models
{
    public class EmployeeModel
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Can't be Empty")]
        public string Name { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime Dob { get; set; }
        [Required(ErrorMessage = "Can't be Empty")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Can't be Empty")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Can't be Empty")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Can't be Empty")]
        public string State { get; set; }
        [Required(ErrorMessage = "Can't be Empty")]
        public string City { get; set; }
        [Required(ErrorMessage = "Can't be Empty")]
        public string Zip { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Joining Date Can't be Empty")]
        public DateTime JoiningDate { get; set; }
        [Required(ErrorMessage = "Can't be Empty")]
        [Display(Name ="Department Name")]
        public string DepartmentName { get; set; }
        [Required(ErrorMessage = "Can't be Empty")]
        public int DepartmentId { get; set; }


    }
}
