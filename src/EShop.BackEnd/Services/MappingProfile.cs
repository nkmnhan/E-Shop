using AutoMapper;
using EShop.BackEnd.Models;
using EShop.Shared.Requests;
using EShop.Shared.Response;
using EShop.Shared.ViewModels.Brand;
using EShop.Shared.ViewModels.Cart;
using EShop.Shared.ViewModels.Category;
using EShop.Shared.ViewModels.Order;
using EShop.Shared.ViewModels.Product;
using System.Linq;

namespace EShop.BackEnd.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Brand, BrandVm>();
            CreateMap<Category, CategoryVm>();
            CreateMap<Product, ProductVm>()
                .ForMember(
                    dest => dest.BrandId,
                    opt => opt.MapFrom(src => src.Brand.Id))
                .ForMember(
                    dest => dest.BrandName,
                    opt => opt.MapFrom(src => src.Brand.Name))
                .ForMember(
                    dest => dest.Categories,
                    opt => opt.MapFrom(src => src.ProductCategories.Select(x => new CategoryVm
                    {
                        Id = x.Category.Id,
                        Name = x.Category.Name
                    })));

            CreateMap<ProductCreateRequest, Product>()
                .ForMember(
                    dest => dest.Brand,
                    opt => opt.MapFrom(src => new Brand { Id = src.BrandId }))
                .ForMember(
                    des => des.ProductCategories,
                    opt => opt.MapFrom(src => src.CategoryIds.Select(cId => new ProductCategory { CategoryId = cId })));
            CreateMap<ProductUpdateRequest, Product>()
                .ForMember(
                    dest => dest.Brand,
                    opt => opt.MapFrom(src => new Brand { Id = src.BrandId }))
                .ForMember(
                    des => des.ProductCategories,
                    opt => opt.MapFrom(src => src.CategoryIds.Select(cId => new ProductCategory { ProductId = src.Id, CategoryId = cId })));

            CreateMap<CategoryCreateRequest, Category>();
            CreateMap<CategoryUpdateRequest, Category>();

            CreateMap<BrandCreateRequest, Brand>();
            CreateMap<BrandUpdateRequest, Brand>();

            CreateMap<Brand, BrandCreateResponse>();
            CreateMap<Brand, BrandUpdateResponse>();

            CreateMap<Category, CategoryCreateResponse>();
            CreateMap<Category, CategoryUpdateResponse>();

            CreateMap<Product, ProductCreateResponse>()
                .ForMember(
                    dest => dest.BrandId,
                    opt => opt.MapFrom(src => src.Brand.Id))
                .ForMember(
                    des => des.CategoryIds,
                    opt => opt.MapFrom(src => src.ProductCategories.Select(x => x.CategoryId)));
            CreateMap<Product, ProductUpdateResponse>()
                .ForMember(
                    dest => dest.BrandId,
                    opt => opt.MapFrom(src => src.Brand.Id))
                .ForMember(
                    des => des.CategoryIds,
                    opt => opt.MapFrom(src => src.ProductCategories.Select(x => x.CategoryId)));

            CreateMap<OrderCreateRequest, Order>();

            CreateMap<CartLineCreateRequest, CartLine>()
                .ForMember(
                    dest => dest.Product,
                    opt => opt.MapFrom(src => new Product { Id = src.ProductId }));

            CreateMap<CartLine, CartLineVm>();

            CreateMap<Order, OrderVm>()
                .ForMember(
                    dest => dest.UserId,
                    opt => opt.MapFrom(src => src.User.Id));
        }
    }
}
