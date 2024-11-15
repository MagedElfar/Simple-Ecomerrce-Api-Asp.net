using Core.DTOS.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
    public interface IBrandsService
    {

        public Task<IEnumerable<BrandDto>> GetAll();
        public Task<BrandDto> GetByIdAync(int id);
        public Task<BrandDto> CreateBrandAsyc(AddBrandDto addCategoryDto);
        public Task<BrandDto> UpdateBrandAsyc(int id, UpdateBrandDto updateCategoryDto);
        public Task DeleteBrandAsyc(int id);
    }
}
