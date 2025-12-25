using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductManager.API.DTOs;
using ProductManager.Core.Entities;
using ProductManager.Core.Interfaces;

namespace ProductManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;
        private readonly IMapper _mapper;

        public ProductsController(IProductService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductDTO request)
        {
            var newProduct = _mapper.Map<Product>(request);
            var createdProduct = await _service.AddProductAsync(newProduct);
            var responseDto =  _mapper.Map<ProductDTO>(createdProduct);
            return CreatedAtAction(nameof(GetById), new { id  = responseDto.Id }, responseDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _service.GetProductByIdAsync(id);
            var productDto = _mapper.Map<ProductDTO>(product);
            return Ok(productDto);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _service.GetAllProductsAsync();
            var productDtos = _mapper.Map<List<ProductDTO>>(products);
            return Ok(productDtos);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateProductDTO request)
        {
            var productUp = _mapper.Map<Product>(request);

            await _service.UpdateProductAsync(id, productUp);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteProductAsync(id);
            return NoContent();
        }
    }
}
