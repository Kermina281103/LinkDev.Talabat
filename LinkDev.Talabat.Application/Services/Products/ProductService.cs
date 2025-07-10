using AutoMapper;
using LinkDev.Talabat.Application.Abstraction.Models.Products;
using LinkDev.Talabat.Application.Abstraction.Services.Products;
using LinkDev.Talabat.Domain.Contract.Persistence;
using LinkDev.Talabat.Domain.Entities.Products;
using LinkDev.Talabat.Domain.Specifications.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Application.Services.Products
{
    class ProductService:IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ProductToReturnDto>> GetProductsAsync(string? sort)
        {
            var spec = new ProductWithBrandAndCategorySpecifications(sort);

            var products = await  _unitOfWork.GetRepository<Product, int>().GetAllWithSpecAsync(spec);
            var productToReturn = _mapper.Map<IEnumerable<ProductToReturnDto>>(products);
            return  productToReturn;
        }

        public async Task<ProductToReturnDto> GetProductAsync(int id)
        {
            var spec = new ProductWithBrandAndCategorySpecifications(id);
            var products = await _unitOfWork.GetRepository<Product, int>().GetWithSpecAsync(spec);
            var productToReturn = _mapper.Map<ProductToReturnDto>(products); ;
            return productToReturn;
        }

        public async Task<IEnumerable<BrandDto>> GetBrandsAsync()
        {
            var brands = await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            var brandToReturn = _mapper.Map<IEnumerable<BrandDto>>(brands);
            return brandToReturn;
        }

        public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
        {
            var categories = await _unitOfWork.GetRepository<ProductCategory, int>().GetAllAsync();
            var categoryToReturn = _mapper.Map<IEnumerable<CategoryDto>>(categories);
            return categoryToReturn;
        }

    }
}
