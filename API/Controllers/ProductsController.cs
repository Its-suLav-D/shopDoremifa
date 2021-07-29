using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTO;
using AutoMapper;
using Core.Interfaces;
using Core.Models;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repo;
        private readonly IGenericRepository<ProductBrand> _productsBrandRepo;
        private readonly IGenericRepository<ProductType> _prdouctsTypeRepo;

        private readonly IMapper _mapper;

        private readonly IGenericRepository<Product> _productsRepo;
        public ProductsController(IGenericRepository<Product> productsRepo,
         IGenericRepository<ProductBrand> productsBrandRepo,
          IGenericRepository<ProductType> prdouctsTypeRepo
          , IMapper mapper)
        {
            this._prdouctsTypeRepo = prdouctsTypeRepo;
            this._productsBrandRepo = productsBrandRepo;
            this._productsRepo = productsRepo;
            this._mapper = mapper;
        }

        // Get all the Products as a list 
        [HttpGet]
        public async Task<ActionResult<List<ProductToReturnDto>>> GetProducts()
        {
            var spec = new ProductsWithTypesAndBrandsSpecification();
            var products = await _productsRepo.ListAsync(spec);

            return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>
            (products));
        }

        // Get a Single Product 
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            var product = await _productsRepo.GetEntityWithSpec(spec);

            return _mapper.Map<Product, ProductToReturnDto>(product);

        }

        [HttpGet("brands")]

        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> getBrands()
        {
            return Ok(await _productsBrandRepo.ListAllAsync());
        }


        [HttpGet("types")]

        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> getTypes()
        {
            return Ok(await _prdouctsTypeRepo.ListAllAsync());
        }
    }
}