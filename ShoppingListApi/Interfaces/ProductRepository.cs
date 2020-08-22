using ShoppingListApi.Data;
using ShoppingListApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingListApi.Interfaces
{
    public class ProductRepository: BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(DataContext context): base(context)
        {

        }
    }
}
