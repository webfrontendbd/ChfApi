namespace ChfApi.Dtos.ReportDtos
{
    public class BookingReceiptDto
    {
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
        public decimal TotalPayable { get; set; }
        public string Status { get; set; }
        public string BookedBy { get; set; }
        public string DoctorName { get; set; }
        public string Specialist { get; set; }
        public int TestId { get; set; }
        public string TestName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string InWords { get; set; }
        public decimal AmountPaid { get; set; }
        public DateTime Created { get; set; }
    }
}
