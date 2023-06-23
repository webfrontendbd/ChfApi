using ChfApi.Entities;

namespace ChfApi.Dtos
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public ICollection<Test> Products { get; set; }
    }
}
