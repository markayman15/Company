using Company.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace Company.PL.ViewModels
{
    public class EmployeeViewModel
    {
        //public int? Employee_Id { get; set; }
        [MinLength(8, ErrorMessage = "Name Should Be At Leats 8 Characters")]
        public string Name { get; set; }
        public int? Age { get; set; }
        [Required(ErrorMessage = "Please Specify Your Job Title In The Company")]
        [Display(Name = "Job Title")]
        public string Job_Title { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public IFormFile? Image { get; set; }
        public string? ImageName { get; set; }
        public Department? Department { get; set; }

        public int? Dept_Id { get; set; }
    }
}
