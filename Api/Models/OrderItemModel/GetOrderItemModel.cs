using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_Api.Models.OrderItemModel
{
    public class GetOrderItemModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }

        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

    }
}
