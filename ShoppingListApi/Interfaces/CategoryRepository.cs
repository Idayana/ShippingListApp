using Microsoft.EntityFrameworkCore;
using ShoppingListApi.Data;
using ShoppingListApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingListApi.Interfaces
{
    public class CategoryRepository: BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(DataContext context): base(context)
        {

        }

        /// <inheritdoc/>
        public int FindByName(string name) => GetQueryable().Where(n => n.CategoryName.Equals(name)).Count();
    }
}
