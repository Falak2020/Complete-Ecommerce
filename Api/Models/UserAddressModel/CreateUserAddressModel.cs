using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models.UserAddressModel
{
    public class CreateUserAddressModel
    {
        public int UserId { get; set; }
        public int AddressId { get; set; }
    }
}
