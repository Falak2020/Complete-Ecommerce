using Api.Models.UserAddressModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_Api.Models.OrderModel
{
    public class CreateOrderModel
    {
        public DateTime OrderDate { get; set; }
        public string OurReference { get; set; }
        public string Status { get; set; }
        public int DeliveryAddressId { get; set; }
        public int InvoiceAddressId { get; set; }
        public int DeliveryTypeId { get; set; }
        public int UserId { get; set; }

    }
}
