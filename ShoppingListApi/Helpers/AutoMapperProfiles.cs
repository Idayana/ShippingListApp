using AutoMapper;
using ShoppingListApi.Dtos.Category;
using ShoppingListApi.Dtos.Product;
using ShoppingListApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingListApi.Helpers
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateBothMaps<CategoryUpdateDto, Category>();
            CreateBothMaps<CategoryCreateDto, Category>();
            CreateBothMaps<Product, ProductCreateDto>();
            CreateBothMaps<Product, ProductUpdateDto>();
        }

        public void CreateBothMaps<TSource, TDestination>()
        {
            this.CreateMap<TSource, TDestination>();
            this.CreateMap<TDestination, TSource>();
        }
    }
}
