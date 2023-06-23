using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ChfApi.Dtos;
using ChfApi.Entities;
using ChfApi.Helpers;
using ChfApi.Interfaces;
using ChfApi.Repositories;
using ChfApi.Specifications;

namespace ChfApi.Controllers
{
    [Authorize]
    public class CategoryController : BaseApiController
    {
        private readonly ICategoryRepository _categoryServices;
        private readonly IGenericRepository<Category> _commonServices;
        private readonly IMapper _mapper;
        public CategoryController(ICategoryRepository categoryServices, IGenericRepository<Category> commonServices, IMapper mapper)
        {
            _categoryServices = categoryServices;
            _commonServices = commonServices;
            _mapper = mapper;

        }

        [HttpGet]
        public async Task<ActionResult<Pagination<CategoryDto>>> GetAllAsync([FromQuery] EntitySpecParams categorySpecParams)
        {
            var spec = new CategorySpecification(categorySpecParams);
            var countSpec = new CategoryFilterCountSpecification(categorySpecParams);
            var totalItems = await _commonServices.CountAsync(countSpec);
            var categories = await _commonServices.ListAsync(spec);
            if (categories == null) return NotFound();
            var data = _mapper.Map<IReadOnlyList<Category>, IReadOnlyList<CategoryDto>>(categories);
            return Ok(new Pagination<CategoryDto>(categorySpecParams.PageIndex, categorySpecParams.PageSize, totalItems, data));
        }

        [HttpPost("add-category")]
        public async Task<ActionResult<CategoryDto>> AddCategory(CategoryDto categoryDto)
        {
            if (await _categoryServices.IsCategoryExists(categoryDto.CategoryName)) return BadRequest("Duplicate Found");

            var category = _mapper.Map<Category>(categoryDto);
            _commonServices.Add(category);
            if (!await _commonServices.SaveAsync()) return BadRequest("Problem in adding");
            return Ok(category);
        }

        [HttpPost("update-category")]
        public async Task<ActionResult<CategoryDto>> UpdateCategory(CategoryDto categoryDto)
        {
            if (await _categoryServices.IsCategoryExists(categoryDto.CategoryName)) return BadRequest("Duplicate Found");

            var category = _mapper.Map<Category>(categoryDto);
            _commonServices.Update(category);
            if (!await _commonServices.SaveAsync()) return BadRequest("Problem in updating");
            return Ok(category);
        }
    }
}
