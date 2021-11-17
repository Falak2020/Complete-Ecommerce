using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_MVC.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Date")]
        public DateTime OrderDate { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string OurReference { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public int DeliveryTypeId { get; set; }

        [Required]
        public int UserId { get; set; }

     
        public DeliveryType deliveryType { get; set; }

        public int DeliveryAddressId{ get; set; }

       
        public int InvoiceAddressId{ get; set; }

    }
}
