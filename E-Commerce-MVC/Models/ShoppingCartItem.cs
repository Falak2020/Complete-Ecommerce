using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_MVC.Models
{
    public class ShoppingCartItem
    {
      
        public Product product { get; set; }
        public int quantity { get; set; }
    }
}
