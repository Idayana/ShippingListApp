using System.Collections.Generic;

namespace ShoppingListApi.Models
{
    public class Category: Entity
    {
        public string CategoryName { get; set; }
        public List<Product> Products { get; set; }
    }
}