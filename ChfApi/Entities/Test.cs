namespace ChfApi.Entities
{
    public class Test : BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool IsActived { get; set; } = true;
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
    }
}
