using AutoMapper;
using ECommerceApi.Application.DTOs.Dtos;
using ECommerceApi.Domain.Entities;
using ECommerceApi.Infrastructure.Repositories.CategoryRepo;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepo _categoryRepo;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryRepo categoryRepo, IMapper mapper)
        {
            _categoryRepo = categoryRepo;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryDto dto)
        {
            var category = _mapper.Map<Category>(dto);
            return Ok(await _categoryRepo.CreateAsync(category));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _categoryRepo.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryRepo.GetByIdAsync(id);
            return category == null ? NotFound($"No Category was Found With ID: {id}") : Ok(category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CategoryDto dto)
        {
            var category = await _categoryRepo.GetByIdAsync(id);
            return (category == null) ? NotFound($"No Category was Found With ID: {id}")
                : Ok(await _categoryRepo.UpdateAsync(_mapper.Map<Category>(dto)));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) => Ok(await _categoryRepo.DeleteAsync(id));

        [HttpGet("search/{filter}")]
        public async Task<IActionResult> Filtter(string filter)
        {
            var fil = await _categoryRepo.GetByCriteriaAsync(c => c.CategoryName!.Contains(filter));
            return Ok(fil);
        }
    }
}
