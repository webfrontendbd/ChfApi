using AutoMapper;
using ChfApi.Data;
using ChfApi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ChfApi.Repositories
{
    public class TestRepository : ITestRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public TestRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<bool> IsProductExists(string productname)
        {
          return await _context.Tests.AnyAsync(p => p.Name == productname.Trim());
        }

    }
}
