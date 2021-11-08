using E_Commerce_Api.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_Api.Models.ProductModel
{
    public class GetProductsModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageURL { get; set; }
        public bool InStock { get; set; }
        public virtual SubCategoryEntity  SubCategory { get; set; }
    }
}
