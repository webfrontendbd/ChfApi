namespace ChfApi.Entities
{
    public class Booking : BaseEntity
    {
        public DateTime BookingDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string BookingNumber { get; set; }
        public string PatientName { get; set; }
        public int Age { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Discount { get; set; }
        public string DiscountType { get; set; }
        public decimal TotalPayable { get; set; }
        public string Status { get; set; }
        public int UserId { get; set; }
        public AppUser User { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public ICollection<BookingDetail> BookingDetails { get; set; }
        public ICollection<Payment> Payments { get; set; }
    }
}
