using E_Commerce_Api.Data.Entities;
using E_Commerce_Api.Models.AddressModel;
using E_Commerce_Api.Models.OrderModel;
using E_Commerce_Api.Models.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_Api.Models.OrderItemModel
{
    public class GetOneOrderModel
    {

        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string OurReference { get; set; }


        public string Status { get; set; }

        public int DeliveryTypeId { get; set; }

        public int UserId { get; set; }

        public string DeliveryTypeName { get; set; }

        public virtual GetOrdersUserModel User { get; set; }
        public virtual GetAddressModel Address { get; set; }
        public virtual ICollection<GetOrderItemModel> OrderItems { get; set; }
    }
}
