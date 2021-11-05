using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_Api.Data.Entities
{
    public class ProductEntity
    {


        [Key]
        [DisplayName("Product Id")]
        public int Id { get; set; }

        [Required]
        [DisplayName("Product Name")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Must be between {1} and {2} character in length.")]
        [Column(TypeName = "nvarchar(200)")]
        public string Name { get; set; }


        [DisplayName("Product Description")]
        [Column(TypeName = "nvarchar(max)")]
        [StringLength(8192, MinimumLength = 5, ErrorMessage = "Must be at least {2} character in length.")]
        public string Description { get; set; }


        [DisplayName("Product Price (SEK)")]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        [Required]
        public string ImageURL { get; set; }

        [Required]
        public bool InStock { get; set; }

        [Required]
        public int SubCategoryId { get; set; }

        public virtual SubCategoryEntity SubCategory { get; set; }

        public virtual  ICollection<OrderItemEntity> OrderItems { get; set; }

    }
}
