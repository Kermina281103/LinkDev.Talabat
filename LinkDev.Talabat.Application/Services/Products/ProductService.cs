using AutoMapper;
using LinkDev.Talabat.Application.Abstraction.Common;
using LinkDev.Talabat.Application.Abstraction.Models.Products;
using LinkDev.Talabat.Application.Abstraction.Services.Products;
using LinkDev.Talabat.Application.Exceptions;
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
        public async Task<Pagination<ProductToReturnDto>> GetProductsAsync(ProductSpecParams specParams)
        {
            var spec = new ProductWithBrandAndCategorySpecifications(
                specParams.Sort,specParams.BrandId,specParams.CategoryId,specParams.PageIndex,specParams.PageSize,specParams.Search);

            var products = await  _unitOfWork.GetRepository<Product, int>().GetAllWithSpecAsync(spec);
            
            var specCount =new ProductForCountSpecification(specParams.BrandId, specParams.CategoryId,specParams.Search);
            var count = await _unitOfWork.GetRepository<Product, int>().GetCountAsync(specCount);
           
            
            var data = _mapper.Map<IEnumerable<ProductToReturnDto>>(products);
            return new Pagination<ProductToReturnDto>(specParams.PageIndex, specParams.PageSize,count)
            {
                Data = data,
              

            };
        }

        public async Task<ProductToReturnDto> GetProductAsync(int id)
        {
            var spec = new ProductWithBrandAndCategorySpecifications(id);
            var products = await _unitOfWork.GetRepository<Product, int>().GetWithSpecAsync(spec);
            if (products is null)
                throw new NotFoundException(nameof(Product),id);

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
