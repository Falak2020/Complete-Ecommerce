using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models.ProductModel
{
    public class EditProduct
    {
        public string Description { get; set; }
        public decimal Price { get; set; } 
        public bool InStock { get; set; }
      
    }
}
