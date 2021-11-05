using System;
using System.Collections.Generic;
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
      //  public virtual ICollection<OrderEntity> Orders { get; set; }


    }
}
