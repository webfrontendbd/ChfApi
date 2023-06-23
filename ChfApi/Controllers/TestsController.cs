using AutoMapper;
using ChfApi.Dtos;
using ChfApi.Entities;
using ChfApi.Helpers;
using ChfApi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ChfApi.Specifications;
using ChfApi.Specifications.EntityParams;

namespace ChfApi.Controllers
{
    [Authorize]
    public class TestsController : BaseApiController
    {
        private readonly ITestRepository _productServices;
        private readonly IGenericRepository<Test> _commonServices;
        private readonly IMapper _mapper;
        public TestsController(ITestRepository productServices, IGenericRepository<Test> commonServices, IMapper mapper)
        {
            _productServices = productServices;
            _commonServices = commonServices;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<Pagination<TestDto>>> GetAllAsync([FromQuery] ProductParams categorySpecParams)
        {
            var spec = new TestSpecification(categorySpecParams);
            var countSpec = new TestFilterCountSpecification(categorySpecParams);

            var totalItems = await _commonServices.CountAsync(countSpec);
            var products = await _commonServices.ListAsync(spec);
            if (products == null) return NotFound();
            var data = _mapper.Map<IReadOnlyList<Test>, IReadOnlyList<TestDto>>(products);
            return Ok(new Pagination<TestDto>(categorySpecParams.PageIndex, categorySpecParams.PageSize, totalItems, data));
        }

        [HttpPost("add-product")]
        public async Task<ActionResult<TestDto>> AddCategory(TestDto productDto)
        {
            if (await _productServices.IsProductExists(productDto.Name)) return BadRequest("Duplicate Found");

            var product = _mapper.Map<Test>(productDto);
            _commonServices.Add(product);
            if (!await _commonServices.SaveAsync()) return BadRequest("Problem in adding");
            return Ok(product);
        }
        [HttpPost("update-product")]
        public async Task<ActionResult> Update(TestDto productDto)
        {
            var product = _mapper.Map<Test>(productDto);
            _commonServices.Update(product);
            if (!await _commonServices.SaveAsync()) return BadRequest("Problem in updating");
            return Ok();
        }


    }
}
