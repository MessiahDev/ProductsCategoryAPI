using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsCategoryAPI.Entities;
using ProductsCategoryAPI.Repositories;
using AutoMapper;
using ProductsCategoryAPI.DTOs;

namespace ProductsCategoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
        {
            var products = await _unitOfWork.ProductRepository.GetAllAsync();

            if (products == null)
            {
                return NotFound();
            }

            var productsDto = _mapper.Map<IEnumerable<ProductDTO>>(products);

            return Ok(productsDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProduct(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetAsync(e => e.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            var productDto = _mapper.Map<ProductDTO>(product);

            return Ok(productDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, ProductDTO productDto)
        {
            if (id != productDto.ProductId)
            {
                return BadRequest();
            }

            var product = await _unitOfWork.ProductRepository.GetAsync(e => e.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            _mapper.Map(productDto, product);

            try
            {
                await _unitOfWork.ProductRepository.UpdateAsync(product);
                await _unitOfWork.Commit();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<ProductDTO>> PostProduct(ProductDTO productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            await _unitOfWork.ProductRepository.CreateAsync(product);
            await _unitOfWork.Commit();

            var newProductDto = _mapper.Map<ProductDTO>(product);

            return CreatedAtAction("GetProduct", new { id = newProductDto.ProductId }, newProductDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetAsync(e => e.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            await _unitOfWork.ProductRepository.DeleteAsync(product);
            await _unitOfWork.Commit();

            return NoContent();
        }

        private async Task<bool> ProductExists(int id)
        {
            return await _unitOfWork.ProductRepository.GetAsync(e => e.ProductId == id) != null;
        }
    }
}
