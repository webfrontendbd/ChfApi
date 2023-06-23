using ChfApi.Dtos;
using ChfApi.Entities;

namespace ChfApi.Interfaces
{
    public interface IBookingRepository
    {
        Task<Booking> GetLastOrder();
        Task<BookingDto> GetOrderByInvoiceNumber(string invoiceNumber);
    }
}
