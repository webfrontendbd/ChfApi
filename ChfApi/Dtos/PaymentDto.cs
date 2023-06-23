namespace ChfApi.Dtos
{
    public class PaymentDto
    {
        public int Id { get; set; }
        public string PaymentDate { get; set; }
        public int BookingId { get; set; }
        public decimal Amount { get; set; }
        public string Method { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
    }
}
