using Core.Dtos.Brands;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Repositories
{
    public interface IBrandRepository:IGenericRepository<Brand>
    {
        Task<IEnumerable<BrandWithProductCountDto>> GetAllWithProductsCount();
    }
}
