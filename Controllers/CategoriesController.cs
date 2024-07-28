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
    public class CategoriesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoriesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategories()
        {
            var categories = await _unitOfWork.CategoryRepository.GetAllAsync();

            if (categories == null)
            {
                return NotFound();
            }

            var categoriesDto = _mapper.Map<IEnumerable<CategoryDTO>>(categories);

            return Ok(categoriesDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDTO>> GetCategory(int id)
        {
            var categoria = await _unitOfWork.CategoryRepository.GetAsync(e => e.CategoryId == id);

            if (categoria == null)
            {
                return NotFound();
            }

            var categoriaDto = _mapper.Map<CategoryDTO>(categoria);

            return categoriaDto;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CategoryDTO>> PutCategory(int id, CategoryDTO categoryDto)
        {
            if (id != categoryDto.CategoryId)
            {
                return BadRequest();
            }

            var category = await _unitOfWork.CategoryRepository.GetAsync(e => e.CategoryId == id);

            if (category == null)
            {
                return NotFound();
            }

            _mapper.Map(categoryDto, category);

            try
            {
                await _unitOfWork.CategoryRepository.UpdateAsync(category);
                await _unitOfWork.Commit();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            var categoryUpdatedDto = _mapper.Map<CategoryDTO>(category);
            return Ok(categoryUpdatedDto);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> PostCategory(CategoryDTO categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            await _unitOfWork.CategoryRepository.CreateAsync(category);
            await _unitOfWork.Commit();

            var newCategoryDto = _mapper.Map<CategoryDTO>(category);

            return CreatedAtAction("GetCategory", new { id = newCategoryDto.CategoryId }, newCategoryDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetAsync(e => e.CategoryId == id);

            if (category == null)
            {
                return NotFound();
            }

            await _unitOfWork.CategoryRepository.DeleteAsync(category);
            await _unitOfWork.Commit();

            var categoryDto = _mapper.Map<CategoryDTO>(category);

            return Ok(categoryDto);
        }

        private async Task<bool> CategoryExists(int id)
        {
            return await _unitOfWork.CategoryRepository.GetAsync(e => e.CategoryId == id) != null;
        }
    }
}
