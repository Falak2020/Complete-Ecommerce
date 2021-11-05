using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_Api.Data.Entities
{

    [Index(nameof(Name), IsUnique = true)]
    public class SubCategoryEntity
    {
    
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName ="nvarchar(100)")]
        public string Name { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public virtual CategoryEntity Category { get; set; }

        public virtual ICollection<ProductEntity> Products { get; set; }
    }
}
