using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ChfApi.Dtos;
using ChfApi.Entities;
using ChfApi.Helpers;
using ChfApi.Interfaces;
using ChfApi.Specifications;

namespace ChfApi.Controllers
{
    public class EmployeesController : BaseApiController
    {
        private readonly IGenericRepository<Employee> _commonRepo;
        private readonly IMapper _mapper;

        public EmployeesController(IGenericRepository<Employee> commonRepo,
            IMapper mapper)
        {
            _commonRepo = commonRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<EmployeeDto>>> GetAllAsync([FromQuery] EntitySpecParams empoyeeSpecParams)
        {
            var spec = new EmployeeSpecification(empoyeeSpecParams);
            var countSpec = new EmployeeFilterCountSpecification(empoyeeSpecParams);
            var totalItems = await _commonRepo.CountAsync(countSpec);
            var employees = await _commonRepo.ListAsync(spec);
            if (employees == null) return NotFound();
            var data = _mapper.Map<IReadOnlyList<Employee>, IReadOnlyList<EmployeeDto>>(employees);
            return Ok(new Pagination<EmployeeDto>(empoyeeSpecParams.PageIndex, empoyeeSpecParams.PageSize, totalItems, data));
        }

        [HttpPost("add-employee")]
        public async Task<ActionResult<EmployeeDto>> AddEmployee(EmployeeDto employeeDto)
        {
            var employee = _mapper.Map<Employee>(employeeDto);
            _commonRepo.Add(employee);
            if (!await _commonRepo.SaveAsync()) return BadRequest("Problem in adding");
            return Ok(employee);
        }
        [HttpPost("update-employee")]
        public async Task<ActionResult> Update(EmployeeDto model)
        {
            var employee = _mapper.Map<Employee>(model);
            _commonRepo.Update(employee);
            if (!await _commonRepo.SaveAsync()) return BadRequest("Problem in updating");
            return Ok();
        }
    }
}
