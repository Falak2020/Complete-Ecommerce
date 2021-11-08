using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_Api.Data.Entities
{
    public class UserAddressEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int AddressId { get; set; }
        public virtual UserEntity  User { get; set; }
        public virtual AddressEntity Address { get; set; }

        [InverseProperty(nameof(OrderEntity.DeliveryAddress))]
        public virtual ICollection<OrderEntity>DeliveryOrders { get; set; }

        [InverseProperty(nameof(OrderEntity.InvoiceAddress))]
        public virtual ICollection<OrderEntity> InvoiceOrders { get; set; }



    }
}
