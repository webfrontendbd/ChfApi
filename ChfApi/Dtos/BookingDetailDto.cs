namespace ChfApi.Dtos
{
    public class BookingDetailDto
    {
        public int Id { get; set; }
        public int TestId { get; set; }
        public string TestName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int BookingId { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;

    }
}
