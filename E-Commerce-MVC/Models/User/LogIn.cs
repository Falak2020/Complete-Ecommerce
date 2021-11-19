using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_MVC.Models.User
{
    public class LogIn
    {  
      
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "{0} must be provided")]
        [EmailAddress]
     
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "{0} must be a valid address")]
        public string Email { get; set; }

        
        [Display(Name = "Password")]
        [Required(ErrorMessage = "{0} must be provided")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%^&+=]).*$", ErrorMessage = "{0} must be at least 8 characters long, contain one number and a mixture of uppercase and lowercase letters and at least one special character (!*@#$%^&+=)")]
        public string password { get; set; }
    }
}
