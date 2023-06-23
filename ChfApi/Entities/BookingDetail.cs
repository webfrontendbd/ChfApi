namespace ChfApi.Entities
{
    public class BookingDetail : BaseEntity
    {
        public int TestId { get; set; }
        public Test Test { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int BookingId { get; set; }
        public Booking Booking { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;

    }
}
