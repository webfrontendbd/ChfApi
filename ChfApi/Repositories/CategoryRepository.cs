using Microsoft.EntityFrameworkCore;
using ChfApi.Data;
using ChfApi.Interfaces;

namespace ChfApi.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;

        public CategoryRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<bool> IsCategoryExists(string categoryName)
        {
            return await _context.Categories.AnyAsync(p => p.CategoryName == categoryName.Trim());
        }
    }
}
