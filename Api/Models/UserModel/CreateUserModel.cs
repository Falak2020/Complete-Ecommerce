using E_Commerce_Api.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_Api.Models.UserModel
{
    public class CreateUserModel
    {
       
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string password { get; set; }
       
     
    }
}
