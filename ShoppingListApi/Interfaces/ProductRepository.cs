using Microsoft.EntityFrameworkCore;
using ShoppingListApi.Data;
using ShoppingListApi.Helpers;
using ShoppingListApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingListApi.Interfaces
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(DataContext context): base(context)
        {

        }

        public int FindByName(string name) => GetQueryable().Where(n => n.ProductName.Equals(name)).Count();

        public override IQueryable<Product> IncludeGet(IQueryable<Product> entities)
        {
            return entities.Include("Category");
        }

        public async Task<PagedList<Product>> ProductByFilters(ProductParams prodParams)
        {
            var products = GetQueryable();
            if(prodParams.ProductName != null)
            {
                products = products.Where(p => p.ProductName.Contains(prodParams.ProductName, StringComparison.OrdinalIgnoreCase));
                //products = products.Where(p => p.ProductName.Contains(prodParams.ProductName));
            }
            if(prodParams.CategoryId != 0)
            {
                products= products.Where(p => p.CategoryId == prodParams.CategoryId);
            }
            products = IncludeGet(products);
            return await PagedList<Product>.CreateAsync(products, prodParams.PageNumber, prodParams.PageSize);
        }
    }
}
