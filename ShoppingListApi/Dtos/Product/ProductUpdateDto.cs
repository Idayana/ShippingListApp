using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingListApi.Dtos.Product
{
    public class ProductUpdateDto
    {
        public string ProductName { get; set; }
        public int? CategoryId { get; set; }
    }
}
