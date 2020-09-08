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

            CreateMap<Category, CategoryListDto>()
                .ForMember(dest => dest.CategoryId, opt =>
                {
                    opt.MapFrom(src => src.Id);
                });

            CreateBothMaps<Product, ProductCreateDto>() ;
            //CreateBothMaps<Product, ProductUpdateDto>();
            CreateMap<ProductUpdateDto, Product>()
                .ForMember(dest => dest.ProductName, opt => opt.PreCondition(t => t.ProductName != null))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(source => source.CategoryId));



            CreateMap<Product, ProductUpdateDto>()
                .ForMember(dest => dest.ProductName, opt =>
                {
                    opt.MapFrom(src => src.ProductName);
                });
          
            CreateMap<Product, ProductListDto>()
                .ForMember(dest => dest.ProductName, opt => {
                opt.MapFrom(src => src.ProductName);
                })
                .ForMember(dest => dest.CategoryName, opt => {
                opt.MapFrom(src => src.Category.CategoryName);
                })
                .ForMember(dest => dest.ProductId, opt => {
                    opt.MapFrom(src => src.Id);
                });
        }

        public void CreateBothMaps<TSource, TDestination>()
        {
            this.CreateMap<TSource, TDestination>();
            this.CreateMap<TDestination, TSource>();
        }
    }
}
