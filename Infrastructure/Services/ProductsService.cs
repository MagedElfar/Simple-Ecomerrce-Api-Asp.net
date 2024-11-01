using Core.Dtos.Products;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services
{
    public class ProductsService : BaseSerivce<Product>, IProductsService
    {
        private readonly IBaseService<Brand> brandService;
        private readonly IBaseService<ProductType> productTypeService;
        private readonly IMediaStorageService mediaStorageService;
        private const string Folder = "Products";

        public ProductsService(
            IGenericRepository<Product> productRepository,
            IBaseService<Brand> brandService,
            IBaseService<ProductType> productTypeService,
            IMediaStorageService mediaStorageService)
            : base(productRepository)
        {
            this.brandService = brandService;
            this.productTypeService = productTypeService;
            this.mediaStorageService = mediaStorageService;
        }

        public async Task<Product> AddProductAsync(AddProductDto addProductDto)
        {
            var product = await FindOneAync(x => x.SKU == addProductDto.ProductSKU);

            if (product is not null)
                throw new ConflictException("Product SKU should be unique");

            await ValidateProductDependencies(addProductDto.BrandId, addProductDto.CategoryId);

            var imageUrl = await UploadProductImageAsync(addProductDto.File);

            product = new Product
            {
                Name = addProductDto.ProductName,
                SKU = addProductDto.ProductSKU,
                Price = addProductDto.ProductPrice,
                Description = addProductDto.ProductDescription,
                BrandId = addProductDto.BrandId,
                ProductTypeId = addProductDto.CategoryId,
                imageUrl = imageUrl
            };

            return await CreateAync(product);
        }

        public async Task<Product> UpdateProductAsync(int id ,UpdateProductDto updateProductDto)
        {
            var product = await FindByIdAsync(id);

            if (product is null)
                throw new NotFoundException("Prodcut not Found");

            var productWithSku = await FindOneAync(x => x.SKU == updateProductDto.ProductSKU && x.Id != id);

            if (productWithSku is not null)
                throw new ConflictException("Product SKU should be unique");

            await ValidateProductDependencies(updateProductDto.BrandId, updateProductDto.CategoryId);

            product.Name = updateProductDto.ProductName;
            product.SKU = updateProductDto.ProductSKU;
            product.Price = updateProductDto.ProductPrice;
            product.Description = updateProductDto.ProductDescription;
            product.ProductTypeId = updateProductDto.CategoryId;
            product.BrandId = updateProductDto.BrandId;

            await UpdateAsync(product);

            return product;

        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await FindByIdAsync(id) ?? throw new NotFoundException("Product not found");

            // Delete the existing image if it exists
            if (!string.IsNullOrEmpty(product.imageUrl))
                await mediaStorageService.DeleteAsync(product.imageUrl);

            await DeleteAsync(product);

            return;
        }

        public async Task<Product> UpdateProductImage(int id, IFormFile file)
        {
            var product = await FindByIdAsync(id) ?? throw new NotFoundException("Product not found");

            // Delete the existing image if it exists
            if (!string.IsNullOrEmpty(product.imageUrl))
                await mediaStorageService.DeleteAsync(product.imageUrl);

            // Upload new image and update the product
            product.imageUrl = await UploadProductImageAsync(file);
            await UpdateAsync(product);

            return product;
        }

        public async Task DeleteProductImage(int id)
        {
            var product = await FindByIdAsync(id) ?? throw new NotFoundException("Product not found");

            // Delete the image and clear the URL field
            if (!string.IsNullOrEmpty(product.imageUrl))
            {
                await mediaStorageService.DeleteAsync(product.imageUrl);
                product.imageUrl = null;
                await UpdateAsync(product);
            }
        }

        // Helper to validate brand and category dependencies
        private async Task ValidateProductDependencies(int? brandId , int? categoryId)
        {
            if (brandId.HasValue)
            {
                var brand = await brandService.FindByIdAsync(brandId.Value);
                if (brand is null)
                    throw new NotFoundException("Product brand not found");
            }

            if (categoryId.HasValue)
            {
                var category = await productTypeService.FindByIdAsync(categoryId.Value);
                if (category is null)
                    throw new NotFoundException("Product category not found");
            }
        }
        private async Task<string?> UploadProductImageAsync(IFormFile? file)
        {
            return file is null ? null : await mediaStorageService.UploadAsync(file, Folder);
        }
    }
}
