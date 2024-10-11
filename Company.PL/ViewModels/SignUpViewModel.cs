using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Company.PL.ViewModels
{
    public class SignUpViewModel
    {
        public string FName { get; set; }
        public string LName { get; set; }
        [PersonalData]
        [EmailAddress]
        public string Email { get; set; }
        public string UserName { get; set; }
        public int Age { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage = "Password and Confirm Password Not Matched")]
        public string ConfirmPassword { get; set; }
    }
}
