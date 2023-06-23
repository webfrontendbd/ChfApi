using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ChfApi.Dtos;
using ChfApi.Entities;
using ChfApi.Helpers;
using ChfApi.Interfaces;
using ChfApi.Specifications;

namespace ChfApi.Controllers
{
    [Authorize]
    public class ExpensesController : BaseApiController
    {
        private readonly IGenericRepository<Expense> _commonRepo;
        private readonly IMapper _mapper;

        public ExpensesController(IGenericRepository<Expense> commonRepo, IMapper mapper)
        {
            _commonRepo = commonRepo;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<Pagination<ExpenseDto>>> GetAllAsync([FromQuery] EntitySpecParams expenseSpecParams)
        {
            var spec = new ExpenseSpecification(expenseSpecParams);
            var countSpec = new ExpenseFilterCountSpecification(expenseSpecParams);
            var totalItems = await _commonRepo.CountAsync(countSpec);
            var expenses = await _commonRepo.ListAsync(spec);
            if (expenses == null) return NotFound();
            var data = _mapper.Map<IReadOnlyList<Expense>, IReadOnlyList<ExpenseDto>>(expenses);
            return Ok(new Pagination<ExpenseDto>(expenseSpecParams.PageIndex, expenseSpecParams.PageSize, totalItems, data));
        }

        [HttpPost("add-expense")]
        public async Task<ActionResult<ExpenseDto>> AddCategory(ExpenseDto expenseDto)
        {
            //if (await _customerRepository.IsCustomerExists(customerDto.Phone)) return BadRequest("Duplicate Found");

            var expense = _mapper.Map<Expense>(expenseDto);
            _commonRepo.Add(expense);
            if (!await _commonRepo.SaveAsync()) return BadRequest("Problem in adding");
            return Ok(expense);
        }

        [HttpPost("update-expense")]
        public async Task<ActionResult> Update(ExpenseDto expenseDto)
        {
            var expense = _mapper.Map<Expense>(expenseDto);
            _commonRepo.Update(expense);
            if (!await _commonRepo.SaveAsync()) return BadRequest("Problem in updating");
            return Ok();
        }
    }
}
