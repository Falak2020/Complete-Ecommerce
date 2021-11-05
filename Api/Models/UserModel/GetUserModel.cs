
using Api.Models.UserAddressModel;
using E_Commerce_Api.Models.OrderModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_Api.Models.UserModel
{
    public class GetUserModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
      

        public virtual ICollection<GetUsersOrdersModel> Orders { get; set; }
        public virtual ICollection<GetUserAddressModel> UserAddresses { get; set; }


    }
}
