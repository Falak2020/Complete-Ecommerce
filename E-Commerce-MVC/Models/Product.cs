using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_MVC.Models
{
    public class Product
    {

        public int Id { get; set; }

        
        public string Name { get; set; }


        public string Description { get; set; }


        public decimal Price { get; set; }

        public string ImageURL { get; set; }

       
        public bool InStock { get; set; }

       
        public int SubCategoryId { get; set; }

        public virtual SubCategory SubCategory { get; set; }

        public virtual ICollection<Orderitem> OrderItems { get; set; }

    }
}
