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
    public class BrandsServices: IBrandsService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public BrandsServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<BrandDto> CreateBrandAsyc(AddBrandDto addBrandDto)
        {
            var brand = await unitOfWork.Repository<Brand>().GetOneAsync(x => x.Name == addBrandDto.BrandName);

            if (brand != null)
                throw new ConflictException("Brand name must to be uinque");

            brand = await unitOfWork.Repository<Brand>().AddAsync(mapper.Map<Brand>(addBrandDto));

            await unitOfWork.Compleate();

            return mapper.Map<BrandDto>(brand);
        }

        public async Task DeleteBrandAsyc(int id)
        {
            var brand = await CheckBrand(id);

            unitOfWork.Repository<Brand>().Delete(brand);

            await unitOfWork.Compleate();

            return;
        }

        public async Task<IEnumerable<BrandDto>> GetAll()
        {
            var brands = await unitOfWork.Repository<Brand>().GetAllAsync();

            return mapper.Map<List<BrandDto>>(brands);
        }

        public async Task<BrandDto> GetByIdAync(int id)
        {
            var brand = await CheckBrand(id);

            return mapper.Map<BrandDto>(brand);
        }

        public async Task<BrandDto> UpdateBrandAsyc(int id, UpdateBrandDto updateBrandDto)
        {

            var brand = await unitOfWork.Repository<Brand>().GetOneAsync(x => x.Name == updateBrandDto.BrandName && x.Id != id);

            if (brand != null)
                throw new ConflictException("Brand name must be uique");

            brand = await CheckBrand(id);

            mapper.Map(updateBrandDto, brand);

            unitOfWork.Repository<Brand>().Update(brand);


            await unitOfWork.Compleate();


            return mapper.Map<BrandDto>(brand);

        }

        private async Task<Brand?> CheckBrand(int id)
        {
            var brand = await unitOfWork.Repository<Brand>().GetByIdAsync(id);

            if (brand == null)
                throw new NotFoundException("Brand not found");
            return brand;
        }
    }
}
