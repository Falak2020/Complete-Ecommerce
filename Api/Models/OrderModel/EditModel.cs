using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models.OrderModel
{
    public class EditModel
    {
       
        public string Status { get; set; }

        public int DeliveryTypeId { get; set; }
        public int DeliveryAddressId { get; set; }
        public int InvoiceAddressId { get; set; }
        
    }
}
