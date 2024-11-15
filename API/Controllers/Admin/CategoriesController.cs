using AutoMapper;
using Core.DTOS.Category;
using Core.Errors;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using static StackExchange.Redis.Role;

namespace API.Controllers.Admin
{
    public class CategoriesController : BaseAdminController
    {
        private readonly ICategoriesService categoriesService;

        public CategoriesController(
            ICategoriesService categoriesService
        )
        {
            this.categoriesService = categoriesService;
        }

        [HttpGet("{id:int}", Name = "GetById")]
        public async Task<ActionResult<CategoryDto>> GetById(int id)
        {
            return Ok(await categoriesService.GetByIdAync(id));
        }

        [HttpPost("")]
        public async Task<ActionResult<CategoryDto>> Create(AddCatwgoryDto addCategoryDto)
        {
            var category = await categoriesService.CreateCategoryAsyc(addCategoryDto);

            return CreatedAtAction(nameof(GetById), new { id = category.CategoryId }, category );
        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult<CategoryDto>> GetById(int id, UpdateCategoryDto updateCategoryDto)
        {
            return Ok(await categoriesService.UpdateCategoryAsyc(id, updateCategoryDto));
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            await categoriesService.DeleteCategoryAsyc(id);

            return NoContent();
        }
    }
}
