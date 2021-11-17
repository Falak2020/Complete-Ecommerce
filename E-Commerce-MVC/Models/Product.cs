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

        [Required]
        [DisplayName("Product Name")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Must be between {1} and {2} character in length.")]
        public string Name { get; set; }

        [DisplayName("Product Description")]
        [Column(TypeName = "nvarchar(max)")]
        [StringLength(8192, MinimumLength = 5, ErrorMessage = "Must be at least {2} character in length.")]
        public string Description { get; set; }

        [DisplayName("Product Price (SEK)")]
        [Column(TypeName = "money")]
        [Range(1, int.MaxValue, ErrorMessage = "Amount must be greater than 0!")]
        public decimal Price { get; set; }

        [Required]
        [DisplayName("Product Image")]
        public string ImageURL { get; set; }

        [Required]
        public bool InStock { get; set; }

       
        public int SubCategoryId { get; set; }

        public virtual SubCategory SubCategory { get; set; }

    }
}
