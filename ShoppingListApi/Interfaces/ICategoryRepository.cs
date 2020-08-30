using ShoppingListApi.Data;
using ShoppingListApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingListApi.Interfaces
{
    public interface ICategoryRepository: IBaseRepository<Category>
    {

        /// <summary>
        /// Total of categories with a given name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        int FindByName(string name);
    }
}
