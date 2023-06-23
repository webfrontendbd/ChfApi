using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    public class ExpcategoriesController : BaseApiController
    {
        private readonly IGenericRepository<ExpenseCategory> _commonRepo;
        private readonly IMapper _mapper;

        public ExpcategoriesController(IGenericRepository<ExpenseCategory> commonRepo, IMapper mapper)
        {
            _commonRepo = commonRepo;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<Pagination<ExpenseCategoryDto>>> GetAllAsync([FromQuery] EntitySpecParams categorySpecParams)
        {
            var spec = new ExpenseCategorySpecification(categorySpecParams);
            var countSpec = new ExpenseCategoryFilterCountSpecification(categorySpecParams);
            var totalItems = await _commonRepo.CountAsync(countSpec);
            var categories = await _commonRepo.ListAsync(spec);
            if (categories == null) return NotFound();
            var data = _mapper.Map<IReadOnlyList<ExpenseCategory>, IReadOnlyList<ExpenseCategoryDto>>(categories);
            return Ok(new Pagination<ExpenseCategoryDto>(categorySpecParams.PageIndex, categorySpecParams.PageSize, totalItems, data));
        }

        [HttpPost("add-expense-category")]
        public async Task<ActionResult<ExpenseCategoryDto>> AddCategory(ExpenseCategoryDto categoryDto)
        {
            //if (await _customerRepository.IsCustomerExists(customerDto.Phone)) return BadRequest("Duplicate Found");

            var category = _mapper.Map<ExpenseCategory>(categoryDto);
            _commonRepo.Add(category);
            if (!await _commonRepo.SaveAsync()) return BadRequest("Problem in adding");
            return Ok(category);
        }
        [HttpPost("update-expense-category")]
        public async Task<ActionResult> Update(ExpenseCategoryDto model)
        {
            var category = _mapper.Map<ExpenseCategory>(model);
            _commonRepo.Update(category);
            if (!await _commonRepo.SaveAsync()) return BadRequest("Problem in updating");
            return Ok();
        }
    }
}
