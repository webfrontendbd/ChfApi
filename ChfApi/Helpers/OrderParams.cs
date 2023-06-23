namespace ChfApi.Helpers
{
    public class OrderParams:PaginationParams
    {
        public string InvoiceDate { get; set; }
        public string InvoiceNumber { get; set; }
        public string CustomerName { get; set; }
        public string Status { get; set; }
    }
}
