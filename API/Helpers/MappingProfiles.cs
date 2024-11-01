
using Core.Dtos.Products;
using AutoMapper;
using Core.Entities;
using Core.Dtos.Cart;
using Core.Dtos.Order;
using Core.Dtos.Address;

namespace API.Helpers
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles() {
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.ProductSKU, opt => opt.MapFrom(src => src.SKU))
                .ForMember(dest => dest.ProductDescription, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.Image, opt => opt.MapFrom<ProductUrlResolver>())
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.ProductType.Name))
                .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand.Name));

            CreateMap<Cart , CartDto>()
                .ForMember(dest => dest.CartId , opt => opt.MapFrom(s => s.Id))
                .ForMember(dest => dest.cartItems, opt => opt.MapFrom(src => src.Items));

            CreateMap<CartItem, CartItemDto>()
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.Price))
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Product.Id))
            .ForMember(dest => dest.ProductImage, opt => opt.MapFrom<ProductUrlResolver, Product>(src => src.Product));

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
