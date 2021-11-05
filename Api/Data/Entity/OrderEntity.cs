using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_Api.Data.Entities
{
    public class OrderEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }

        [Column(TypeName ="nvarchar(50)")]
        public string OurReference { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]   
        public int DeliveryTypeId { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey("DeliveryAddress")]
        public int DeliveryAddressId { get; set; }

        [ ForeignKey("InvoiceAddress")]
        public int InvoiceAddressId { get; set; }

      
        public virtual DeliveryTypeEntity DeliveryType { get; set; }
        public virtual UserEntity User { get; set; }

      
       // public virtual UserAddressEntity UserAddress { get; set; }

        

        public virtual ICollection<OrderItemEntity>   OrderItems { get; set; }


    }
}
