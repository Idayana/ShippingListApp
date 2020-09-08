using ShoppingListApi.Helpers;
using ShoppingListApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingListApi.Interfaces
{
    public interface IProductRepository: IBaseRepository<Product>
    {
        /// <summary>
        /// Total of products with a given name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        int FindByName(string name);

        /// <summary>
        /// Find products by filters
        /// </summary>
        /// <param name="prodParams"></param>
        /// <returns></returns>
        Task<PagedList<Product>> ProductByFilters(ProductParams prodParams);

        /// <summary>
        /// Include an external relation to the entities
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        IQueryable<Product> IncludeGet(IQueryable<Product> entities);
    }
}
