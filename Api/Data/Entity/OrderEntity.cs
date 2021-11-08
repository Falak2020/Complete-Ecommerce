using Microsoft.EntityFrameworkCore;
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


        [ForeignKey(nameof(DeliveryAddress)), Column(Order = 0)]
        public int? DeliveryAddressId { get; set; }

        [ForeignKey(nameof(InvoiceAddress)), Column(Order = 1)]
        public int? InvoiceAddressId { get; set; }

      
        public virtual DeliveryTypeEntity DeliveryType { get; set; }
        public virtual UserEntity User { get; set; }

      
        public virtual UserAddressEntity DeliveryAddress { get; set; }
        public virtual UserAddressEntity InvoiceAddress { get; set; }




        public virtual ICollection<OrderItemEntity>   OrderItems { get; set; }


    }
   
}
