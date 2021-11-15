using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_MVC.Models
{
    public class AddressModel
    {
        public int Id { get; set; }
        public string AddressLine { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; } 
    }
}
