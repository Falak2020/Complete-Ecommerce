using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_MVC.Models.User
{
    public class UserModel
    {
        public int Id { get; set; }

       
        public string FirstName { get; set; }

       
        public string LastName { get; set; }

        
        public string Email { get; set; }

    }
}
