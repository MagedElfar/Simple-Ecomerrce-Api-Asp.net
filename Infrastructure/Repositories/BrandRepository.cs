using Core.Dtos.Brands;
using Core.Entities;
using Core.Interfaces.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class BrandRepository : GenericRepository<Brand>, IBrandRepository
    {
        public BrandRepository(AdbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<BrandWithProductCountDto>> GetAllWithProductsCount()
        {
          
            return await dbSet.Select(x => new BrandWithProductCountDto
            {
                BrandId = x.Id,
                BrandName = x.Name,
                ProductsCount = x.Products.Count(),
            }).ToListAsync();
        }

    }
}
