using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_Api.Models.SubCategoryModel
{
    public class CreateSubCategoryModel
    {
        public string Name { get; set; }
        public int CategoryId { get; set; }
    }
}
