using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_MVC.Models
{
    public class Orderitem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int OrderId { get; set; }
        public int Quantity { get; set; }

        [Column(TypeName = "money")]
        public decimal UnitPrice { get; set; }

        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }
    }
}
