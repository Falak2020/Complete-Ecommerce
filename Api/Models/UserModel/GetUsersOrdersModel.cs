using E_Commerce_Api.Models.OrderItemModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_Api.Models.UserModel
{
    public class GetUsersOrdersModel
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string OurReference { get; set; }


        public string Status { get; set; }

        public string DeliveryTypeName { get; set; }
        public virtual ICollection<GetOrderItemModel> OrderItems { get; set; }
    }
}
