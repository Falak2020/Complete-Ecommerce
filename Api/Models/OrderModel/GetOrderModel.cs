using E_Commerce_Api.Data.Entities;
using E_Commerce_Api.Models.AddressModel;
using E_Commerce_Api.Models.OrderItemModel;
using E_Commerce_Api.Models.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_Api.Models.OrderModel
{
    public class GetOrderModel
    {

        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string OurReference { get; set; }
        public string Status { get; set; }    
        public string DeliveryTypeName { get; set; }
        public GetAddressModel DeliveryAddress { get; set; }

        public GetAddressModel InvoiceAddress { get; set; }
        public virtual GetOrdersUserModel User { get; set; }
        public virtual ICollection<GetOrderItemModel> OrderItems { get; set; }
    }
}
