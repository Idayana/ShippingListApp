using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingListApi.Models
{
    public class CategoryTranslation: Entity
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Language { get; set; }
    }
}
