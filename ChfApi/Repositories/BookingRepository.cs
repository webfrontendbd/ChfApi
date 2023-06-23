using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ChfApi.Data;
using ChfApi.Dtos;
using ChfApi.Entities;
using ChfApi.Interfaces;

namespace ChfApi.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public BookingRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Booking> GetLastOrder()
        {
            return await _context.Bookings.OrderByDescending(i => i.Id)
                .FirstOrDefaultAsync();

        }

        public async Task<BookingDto> GetOrderByInvoiceNumber(string invoicenumber)
        {
            return await _context.Bookings
                .Where(i => i.BookingNumber == invoicenumber)
                .ProjectTo<BookingDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }
    }
}
