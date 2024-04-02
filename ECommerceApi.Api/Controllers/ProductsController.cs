using AutoMapper;
using ECommerceApi.Application.DTOs.Dtos;
using ECommerceApi.Domain.Entities;
using ECommerceApi.Infrastructure.Repositories.ProductRepo;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepo _productRepo;
        private readonly IWebHostEnvironment _web;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepo productRepo, IMapper mapper, IWebHostEnvironment web)
        {
            _productRepo = productRepo;
            _mapper = mapper;
            _web = web;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ProductDto dto)
        {
            var product = _mapper.Map<Product>(dto);
            product.CreatedDate = DateTime.Now;
            //product.Image = dto.Image == null ? "" : UploadImage(dto.Image!);
            return Ok(await _productRepo.CreateAsync(product));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var product = await _productRepo.GetAllAsync();
            return Ok(product);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productRepo.GetByIdAsync(id);
            return product == null ? BadRequest($"No Product was Found With ID: {id}") : Ok(product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] ProductDto dto)
        {
            var product = await _productRepo.GetByIdAsync(id);
            if (product == null) return NotFound($"No Product was Found With ID: {id}");
            var productdto = _mapper.Map<Product>(dto);
            //productdto.Image = _imageService.UploadImage(dto.Image!, "Product");
            productdto.UpdatedDate = DateTime.Now;
            productdto.IsUpdated = true;
            return Ok(await _productRepo.UpdateAsync(productdto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) => Ok(await _productRepo.DeleteAsync(id));

        [HttpGet("search/{filter}")]
        public async Task<IActionResult> Filtter(string filter)
        {
            var fil = await _productRepo.GetByCriteriaAsync(p => p.ProductName!.Contains(filter));
            return Ok(fil);
        }
    }
}
