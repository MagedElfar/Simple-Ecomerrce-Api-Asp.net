using API.Extensions;
using AutoMapper;
using Core.DTOS.Cart;
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

        public CartsController(ICartService cartService)
        {
            this.cartService = cartService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<CartDto>> GetCart()
        {
            var userId = HttpContext.User.GetUserId();

            var cart = await cartService.MappingCart(userId);

            return Ok(cart);
        }

        [HttpDelete]
        [Authorize]
        public async Task<ActionResult> DeleteCart()
        {
            var userId = HttpContext.User.GetUserId();

            await cartService.DeleteCart(userId);
            return NoContent();
        }


        
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<CartDto>> UpdateCart(UpdateCartDto updateCartDto)
        {

            var userId = HttpContext.User.GetUserId();

            return Ok(await cartService.UpdateCart(userId, updateCartDto));

        }
    }
}
