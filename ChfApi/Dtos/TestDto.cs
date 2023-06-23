namespace ChfApi.Dtos
{
    public class TestDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsActived { get; set; } = true;
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public DateTime Created { get; set; }
    }
}
