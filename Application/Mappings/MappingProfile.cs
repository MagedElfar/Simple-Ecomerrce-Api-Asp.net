using AutoMapper;
using Core.DTOS.Address;
using Core.DTOS.Authentication;
using Core.DTOS.Cart;
using Core.DTOS.Category;
using Core.DTOS.Order;
using Core.DTOS.PaymentMethod;
using Core.DTOS.Product;
using Core.DTOS.User;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappings
{
    public class MappingProfile:Profile
    {
        public MappingProfile() {
            //user mapping
            CreateMap<ApplicationUser , UserDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Roles, opt => opt.Ignore()); // Ignore roles for now

            CreateMap<ApplicationUser, AuthenticationDto>();

            //Category mapping
            CreateMap<Category, CategoryDto>()
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Name));

            CreateMap<AddCatwgoryDto, Category>()
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.CategoryName));
            
            CreateMap<UpdateCategoryDto, Category>()
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.CategoryName));

            //Brand mapping
            CreateMap<Brand, BrandDto>()
                .ForMember(dest => dest.BrandId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Name));

            CreateMap<AddBrandDto, Brand>()
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.BrandName));

            CreateMap<UpdateBrandDto, Brand>()
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.BrandName));

            //Product mapping
            CreateMap<Product, ProductDto>()
              .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Id))
              .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Name))
              .ForMember(dest => dest.ProductSKU, opt => opt.MapFrom(src => src.SKU))
              .ForMember(dest => dest.ProductDescription, opt => opt.MapFrom(src => src.Description))
              .ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(src => src.Price))
              .ForMember(dest => dest.Stock, opt => opt.MapFrom(src => src.Quantity))
              .ForMember(dest => dest.Image, opt => opt.MapFrom<ImageUrlResolver>())
              .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name))
              .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand.Name));

            CreateMap<AddProductDto, Product>()
              .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ProductName))
              .ForMember(dest => dest.SKU, opt => opt.MapFrom(src => src.ProductSKU))
              .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.ProductDescription))
              .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.ProductPrice))
              .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
              .ForMember(dest => dest.BrandId, opt => opt.MapFrom(src => src.BrandId));

            CreateMap<UpdateProductDto, Product>()
              .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ProductName))
              .ForMember(dest => dest.SKU, opt => opt.MapFrom(src => src.ProductSKU))
              .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.ProductDescription))
              .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.ProductPrice))
              .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
              .ForMember(dest => dest.BrandId, opt => opt.MapFrom(src => src.BrandId));


            //cart
            CreateMap<Cart, CartDto>()
               .ForMember(dest => dest.CartId, opt => opt.MapFrom(s => s.Id))
               .ForMember(dest => dest.cartItems, opt => opt.MapFrom(src => src.Items));

            CreateMap<CartItem, CartItemDto>()
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.Price))
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Product.Id))
            .ForMember(dest => dest.ProductImage, opt => opt.MapFrom(src => src.Product.ImageUrl));


            CreateMap<UpdateCartItemDto, CartItem>()
           .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
           .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));

            //payemt method
            CreateMap<PaymentMethod, PaymentMethodDto>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(s => s.Id))
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            //order
            CreateMap<Order, OrderDto>()
              .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Id))
              .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.OrderDate))
              .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.OrderStatus.ToString()))
              .ForMember(dest => dest.SubTotal, opt => opt.MapFrom(src => src.SubTotal))
              .ForMember(dest => dest.CustomerEmail, opt => opt.MapFrom(src => src.Email))
              .ForMember(dest => dest.ShippingAddress, opt => opt.MapFrom(src => src.ShippingAddress))
              .ForMember(dest => dest.PaymentIntentId, opt => opt.MapFrom(src => src.PaymentIntentId))
              .ForMember(dest => dest.PaymentMethod, opt => opt.MapFrom(src => src.PaymentMethod.Name))
              .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

            CreateMap<OrderItem, OrderItemDto>()
              .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
              .ForMember(dest => dest.ProductImage, opt => opt.MapFrom(src => src.ProductImage))
              .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
              .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));

            CreateMap<ShippingAddress, ShippingAddressDto>()
             .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
             .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
             .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Street))
             .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
             .ForMember(dest => dest.Zipcode, opt => opt.MapFrom(src => src.Zipcode));
        }
    }
}
