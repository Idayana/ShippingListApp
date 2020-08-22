using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingListApi.Models
{
    public class ProductTranslation: Entity
    {
        public int ProductId { get; set; }
        public string ProductName{ get; set; }
        public string Language { get; set; }
    }
}
