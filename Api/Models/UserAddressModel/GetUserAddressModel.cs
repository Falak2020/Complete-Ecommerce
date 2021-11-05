using E_Commerce_Api.Data.Entities;
using E_Commerce_Api.Models.AddressModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models.UserAddressModel
{
    public class GetUserAddressModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int AddressId { get; set; }
        public virtual GetAddressModel Address { get; set; }
    }
}
