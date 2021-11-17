using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_MVC.Models.User
{
    public class MyOrder
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string OurReference { get; set; }
        public string Status { get; set; }
        public string DeliveryTypeName { get; set; }
        public AddressModel DeliveryAddress { get; set; }

        public AddressModel InvoiceAddress { get; set; }
        public virtual UserModel User { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
