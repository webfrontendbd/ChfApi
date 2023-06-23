namespace ChfApi.Entities
{
    public class Payment : BaseEntity
    {
        public DateTime PaymentDate { get; set; }
        public int BookingId { get; set; }
        public Booking Booking { get; set; }
        public decimal Amount { get; set; }
        public string Method { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
    }
}
