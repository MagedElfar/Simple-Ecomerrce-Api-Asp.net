using AutoMapper;
using Core.DTOS.Product;
using Core.DTOS.Shared;
using Core.DTOS.User;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Specifications.SpecificationBuilder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProductsService:IProductsService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMediaStorageService mediaStorageService;
        private readonly IMapper mapper;
        private const string Folder = "Products";

        public ProductsService(
            IUnitOfWork unitOfWork,
            IMediaStorageService mediaStorageService,
            IMapper mapper
        ) {
            this.unitOfWork = unitOfWork;
            this.mediaStorageService = mediaStorageService;
            this.mapper = mapper;
        }

        public async Task<ListWithCountDto<ProductDto>> GetAndCountAll(ProductQueryDto productQueryDto)
        {
            var spec = productQueryDto.BuildSpecification()
                .Include("Category")
                .Include("Brand")
                .WithLimit(productQueryDto.Limit)
                .WithPage(productQueryDto.Page)
                .Build();

            var products = await unitOfWork.Repository<Product>().GetAllAsync(spec);
            var count = await unitOfWork.Repository<Product>().GetCountAsync(productQueryDto.BuildSpecification().Build());

            return new ListWithCountDto<ProductDto>
            {
                Count = count,
                Items = mapper.Map<IEnumerable<ProductDto>>(products)
            };
        }

        public async Task<ProductDto> GetProductAsync(int id)
        {

            var spec = new ProductSpecificationBuilder()
                .WithId(id)
                .Include("Category")
                .Include("Brand")
                .Build();

            var product = await unitOfWork.Repository<Product>().GetOneAsync(spec);

            if (product == null)
                throw new NotFoundException("product not found");

            return mapper.Map<ProductDto>(product);
        }
        public async Task<ProductDto> CreateProductAsync(AddProductDto addProductDto)
        {
            var repo = unitOfWork.Repository<Product>();

            await CheckSku(x => x.SKU == addProductDto.ProductSKU);

            await ValidateProductDependencies(addProductDto.BrandId, addProductDto.CategoryId);

            var imageUrl = await UploadProductImageAsync(addProductDto.File);

            var product = mapper.Map<Product>(addProductDto);

            product.ImageUrl = imageUrl;

            await repo.AddAsync(product);

            await unitOfWork.Compleate();

            return mapper.Map<ProductDto>(product);
        }

   
        public async Task<ProductDto> UpdateProductAsync(int id, UpdateProductDto updateProductDto)
        {
            var repo = unitOfWork.Repository<Product>();

            Product product = await GetProduct(id);

            await CheckSku(x => x.SKU == updateProductDto.ProductSKU && x.Id != id);

            await ValidateProductDependencies(updateProductDto.BrandId, updateProductDto.CategoryId);

            mapper.Map(updateProductDto, product);

            repo.Update(product);

            await unitOfWork.Compleate();

            return await GetProductAsync(id);
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await GetProduct(id);

            // Delete the existing image if it exists
            if (!string.IsNullOrEmpty(product.ImageUrl))
                await mediaStorageService.DeleteAsync(product.ImageUrl);

            unitOfWork.Repository<Product>().Delete(product);

            await unitOfWork.Compleate();

            return;
        }

        public async Task<string> UpdateProductImage(int id, IFormFile file)
        {
            var product = await GetProduct(id);

            // Delete the existing image if it exists
            if (!string.IsNullOrEmpty(product.ImageUrl))
                await mediaStorageService.DeleteAsync(product.ImageUrl);

            // Upload new image and update the product
            product.ImageUrl = await UploadProductImageAsync(file);

            unitOfWork.Repository<Product>().Update(product);

            await unitOfWork.Compleate();

            return product.ImageUrl;
        }

        public async Task DeleteProductImage(int id)
        {
            var product = await GetProduct(id);

            // Delete the image and clear the URL field
            if (!string.IsNullOrEmpty(product.ImageUrl))
            {
                await mediaStorageService.DeleteAsync(product.ImageUrl);
   
                product.ImageUrl = null;

                unitOfWork.Repository<Product>().Update(product);

                await unitOfWork.Compleate();
                
            }
        }

        // Helper to validate brand and category dependencies
        private async Task ValidateProductDependencies(int? brandId, int? categoryId)
        {
            if (brandId.HasValue)
            {
                var brand = await unitOfWork.Repository<Brand>().GetByIdAsync(brandId.Value);
                if (brand is null)
                    throw new NotFoundException("Brand not found");
            }

            if (categoryId.HasValue)
            {
                var category = await unitOfWork.Repository<Category>().GetByIdAsync(categoryId.Value);
                if (category is null)
                    throw new NotFoundException("Product category not found");
            }
        }

        private async Task CheckSku(Expression<Func<Product , bool>> expression)
        {
            var repo = unitOfWork.Repository<Product>();

            var product = await repo.GetOneAsync(expression);

            if (product is not null)
                throw new ConflictException("Product SKU should be unique");

            return;

        }

        private async Task<Product> GetProduct(int id)
        {
            var product = await unitOfWork.Repository<Product>().GetByIdAsync(id);

            if (product is null)
                throw new NotFoundException("Prodcut not Found");

            return product;
        }

        private async Task<string?> UploadProductImageAsync(IFormFile? file)
        {
            return file is null ? null : await mediaStorageService.UploadAsync(file, Folder);
        }

    }
}
