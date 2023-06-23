using ChfApi.Entities;

namespace ChfApi.Dtos
{
    public class BookingDto
    {
        public int Id { get; set; }
        public string BookingDate { get; set; }
        public string DeliveryDate { get; set; }
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
        public string BookedBy { get; set; }
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public string Specialist { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public ICollection<BookingDetailDto> BookingDetails { get; set; }
        public ICollection<PaymentDto> Payments { get; set; }
    }
}
