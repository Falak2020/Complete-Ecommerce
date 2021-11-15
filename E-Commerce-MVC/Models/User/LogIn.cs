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
       
        public string password { get; set; }
    }
}
