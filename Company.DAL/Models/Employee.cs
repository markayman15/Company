using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.DAL.Models
{
    public class Employee : ModelBase
    {
        public int Employee_Id { get; set; }
        //[MinLength(8,ErrorMessage ="Name Should Be At Leats 8 Characters")]
        public string Name { get; set; }
        public int? Age { get; set; }
        //[Required(ErrorMessage ="Please Specify Your Job Title In The Company")]
        //[Display(Name = "Job Title")]
        public string Job_Title { get; set; }
        //[EmailAddress]
        public string Email { get; set; }
        public string ImageName { get; set; }
        public Department? Department { get; set; }
        public int? Dept_Id { get; set; }

    }
}
