using ChfApi.Dtos;

namespace ChfApi.Interfaces
{
    public interface IReportServices
    {
        Task<byte[]> GetBookingPrinterReceiptAsync(string invoiceNumber);
        Task<BookingDto> GetInvoiceByInvoiceNumber(string invoiceNumber);
    }
}
