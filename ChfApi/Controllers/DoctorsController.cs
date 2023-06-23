using AutoMapper;
using ChfApi.Dtos;
using ChfApi.Entities;
using ChfApi.Helpers;
using ChfApi.Interfaces;
using ChfApi.Specifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChfApi.Controllers
{
    public class DoctorsController : BaseApiController
    {
        private readonly IGenericRepository<Doctor> _repo;
        private readonly IMapper _mapper;

        public DoctorsController(IGenericRepository<Doctor> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<Pagination<DoctorDto>>> GetAllAsync([FromQuery] EntitySpecParams doctorSpecParams)
        {
            var spec = new DoctorSpecification(doctorSpecParams);
            var countSpec = new DoctorFilterCountSpecification(doctorSpecParams);
            var totalItems = await _repo.CountAsync(countSpec);
            var employees = await _repo.ListAsync(spec);
            if (employees == null) return NotFound();
            var data = _mapper.Map<IReadOnlyList<Doctor>, IReadOnlyList<DoctorDto>>(employees);
            return Ok(new Pagination<DoctorDto>(doctorSpecParams.PageIndex, doctorSpecParams.PageSize, totalItems, data));
        }

        [HttpPost("add-doctor")]
        public async Task<ActionResult<DoctorDto>> AddEmployee(DoctorDto employeeDto)
        {
            var employee = _mapper.Map<Doctor>(employeeDto);
            _repo.Add(employee);
            if (!await _repo.SaveAsync()) return BadRequest("Problem in adding");
            return Ok(employee);
        }
        [HttpPost("update-doctor")]
        public async Task<ActionResult> Update(DoctorDto model)
        {
            var employee = _mapper.Map<Doctor>(model);
                _repo.Update(employee);
            if (!await _repo.SaveAsync()) return BadRequest("Problem in updating");
            return Ok();
        }
    }
}
