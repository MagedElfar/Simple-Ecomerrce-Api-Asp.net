using Core.Dtos.Brands;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;


namespace Infrastructure.Services
{
    public class BrandsService : BaseSerivce<Brand>, IBrandsService
    {
        private readonly IBrandRepository _brandRepository;
        public BrandsService(IBrandRepository repository) : base(repository)
        {
            _brandRepository = repository;
        }

        public async Task<IEnumerable<BrandWithProductCountDto>> GetAllWithProductsCount()
        {
            return await _brandRepository.GetAllWithProductsCount();
        }

        public async Task<Brand> AddBrand(AddBrandDto addBrandDto) { 
            var brand = await FindOneAync(x => x.Name == addBrandDto.BrandName);

            if (brand != null)
                throw new ConflictException("Brand name must be unique.");

            brand = new Brand { Name = addBrandDto.BrandName };


            await CreateAync(brand);

            return brand;
         
        }

    }
}
