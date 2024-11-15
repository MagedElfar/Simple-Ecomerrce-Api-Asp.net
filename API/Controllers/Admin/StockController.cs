using Core.DTOS.Stock;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Admin
{
    public class StockController:BaseAdminController
    {
        private readonly IStockService stockService;

        public StockController(IStockService stockService)
        {
            this.stockService = stockService;
        }


        [HttpPost()]
        public async Task<ActionResult> UpdateProductStock(UpdateProductStockDto updateProductStockDto)
        {
            var qty = await stockService.UpdateStock(updateProductStockDto.ProductId, updateProductStockDto.Quantity);

            return Ok(new { Quantity = qty });
        }
    }
}
