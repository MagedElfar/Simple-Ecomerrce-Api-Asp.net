using API.Extensions;
using AutoMapper;
using Core.Dtos.Cart;
using Core.Dtos.Products;
using Core.Entities;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CartsController : BaseApiController
    {
        private readonly ICartService cartService;
        private readonly IMapper mapper;

        public CartsController(ICartService cartService, IMapper mapper)
        {
            this.cartService = cartService;
            this.mapper = mapper;
        }

        [HttpGet()]
        [Authorize]
        public async Task<ActionResult<CartDto>> GetCart()
        {
            var userId = HttpContext.User.GetUserId();

            var cart = await cartService.GetCartByIdAsync(userId.ToString());


            return Ok(cart is not null ? mapper.Map<Cart, CartDto>(cart) : new CartDto(userId.ToString()));
        }

        [HttpDelete()]
        [Authorize]
        public async Task<ActionResult<CartDto>> DeleteCart()
        {
            var userId = HttpContext.User.GetUserId();

            await cartService.DeleteCart(userId.ToString());
            return Ok();
        }


        
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<CartDto>> UpdateCart(UpdateCartDto updateCartDto)
        {

            var userId = HttpContext.User.GetUserId();

            var updatedCart = new Cart(userId.ToString());

            updatedCart.Items = updateCartDto.CartItems.Select(x => new CartItem
            {
                Quantity = x.Quantity,
                ProductId = x.ProductId,
            }).ToList();


            var cart = await cartService.UpdateCart(updatedCart);

            return Ok(mapper.Map<Cart, CartDto>(cart) );

        }
    }
}
