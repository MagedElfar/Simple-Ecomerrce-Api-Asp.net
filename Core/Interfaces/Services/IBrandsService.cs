using Core.Dtos.Brands;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{

    public interface IBrandsService:IBaseService<Brand>
    {
        Task<IEnumerable<BrandWithProductCountDto>> GetAllWithProductsCount();

        Task<Brand> AddBrand(AddBrandDto addBrandDto);
    }
}
