using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_Api.Data.Entities
{
    public class AddressEntity
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [Required]
        public string AddressLine { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        [Required]
        public string ZipCode { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        [Required]
        public string City { get; set; }
        public virtual ICollection<UserAddressEntity> UserAddresses { get; set; }
    }
}
