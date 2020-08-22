using Microsoft.Extensions.DependencyInjection;
using ShoppingListApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace ShoppingListApi.Extensions
{
    public static class DependencyRegistry
    {
        public static IServiceCollection ConfigureDataDependencies(this IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            return services;
        }
    }
}
