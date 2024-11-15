using AutoMapper;
using Core.DTOS.Category;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public CategoriesService(IUnitOfWork unitOfWork , IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDto>> GetAll()
        {
            var categories = await unitOfWork.Repository<Category>().GetAllAsync();

            return mapper.Map<List<CategoryDto>>(categories);
        }

        public async Task<CategoryDto> CreateCategoryAsyc(AddCatwgoryDto addCategoryDto)
        {
            var category = await unitOfWork.Repository<Category>().GetOneAsync(x => x.Name == addCategoryDto.CategoryName);

            if (category != null)
                throw new ConflictException("Category is already excite");

            category = await unitOfWork.Repository<Category>().AddAsync(mapper.Map<Category>(addCategoryDto));

            await unitOfWork.Compleate();

            return mapper.Map<CategoryDto>(category);
        }

        public async Task DeleteCategoryAsyc(int id)
        {
            var category = await CheckCategory(id);

            unitOfWork.Repository<Category>().Delete(category);

            await unitOfWork.Compleate();

            return;
        }

        public async Task<CategoryDto> GetByIdAync(int id)
        {
            var category = await CheckCategory(id);

            return mapper.Map<CategoryDto>(category);
        }

        public async Task<CategoryDto> UpdateCategoryAsyc(int id, UpdateCategoryDto updateCategoryDto)
        {

            var category = await unitOfWork.Repository<Category>().GetOneAsync(x => x.Name == updateCategoryDto.CategoryName && x.Id != id);

            if (category != null)
                throw new ConflictException("Category name must be uique");

            category = await CheckCategory(id);

            mapper.Map(updateCategoryDto, category);

            unitOfWork.Repository<Category>().Update(category);


            await unitOfWork.Compleate();


            return mapper.Map<CategoryDto>(category);

        }

        private async Task<Category?> CheckCategory(int id)
        {
           var category = await unitOfWork.Repository<Category>().GetByIdAsync(id);

            if (category == null)
                throw new NotFoundException("Category not found");
            return category;
        }
    }
}
