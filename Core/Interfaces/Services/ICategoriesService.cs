using Core.DTOS.Category;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
    public interface ICategoriesService
    {
        public Task<IEnumerable<CategoryDto>> GetAll();
        public Task<CategoryDto> GetByIdAync(int id);
        public Task<CategoryDto> CreateCategoryAsyc(AddCatwgoryDto addCategoryDto);
        public Task<CategoryDto> UpdateCategoryAsyc(int id, UpdateCategoryDto updateCategoryDto);
        public Task DeleteCategoryAsyc(int id);
    }
}
