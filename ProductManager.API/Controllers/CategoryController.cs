using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductManager.API.DTOs;
using ProductManager.Core.Entities;
using ProductManager.Core.Interfaces;

namespace ProductManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;
        private IMapper _mapper;
        public CategoryController(ICategoryService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateCategoryDTO request)
        {
            var newCate = _mapper.Map<Category>(request);
            var createdCate = await _service.Addcategory(newCate);
            var responseCate = _mapper.Map<CategoryDTO>(createdCate);
            return CreatedAtAction(nameof(GetById), new { id = responseCate.Id }, responseCate);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var cate = await _service.GetcategoryById(id);
            var cateDto = _mapper.Map<CategoryDTO>(cate);
            return Ok(cateDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _service.GetAllCategories();
            var cateDto = _mapper.Map<List<CategoryDTO>>(categories);
            return Ok(cateDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateCategoryDTO request)
        {
            var category = _mapper.Map<Category>(request);
            await _service.UpdateCategory(id, category);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteCategory(id);
            return NoContent();
        }
    }
}
