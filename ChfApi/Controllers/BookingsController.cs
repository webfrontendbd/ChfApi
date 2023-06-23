using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ChfApi.Dtos;
using ChfApi.Entities;
using ChfApi.Helpers;
using ChfApi.Interfaces;
using ChfApi.Specifications;
using ChfApi.Extensions;

namespace ChfApi.Controllers
{
    [Authorize]
    public class BookingsController :BaseApiController
    {
        private readonly IBookingRepository _invoiceRepo;
        private readonly IGenericRepository<Booking> _commonRepo;
        private readonly IMapper _mapper;

        public BookingsController(IBookingRepository invoiceRepo,
            IGenericRepository<Booking> commonRepo,
            IMapper mapper)
        {
            _invoiceRepo = invoiceRepo;
            _commonRepo = commonRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<BookingDto>>> GetAllAsync([FromQuery] EntitySpecParams invoiceSpecParams)
        {
            var spec = new BookingSpecification(invoiceSpecParams);
            var countSpec = new BookingFilterCountSpecification(invoiceSpecParams);
            var totalItems = await _commonRepo.CountAsync(countSpec);
            var invoices = await _commonRepo.ListAsync(spec);
            if (invoices == null) return NotFound();
            var data = _mapper.Map<IReadOnlyList<Booking>, IReadOnlyList<BookingDto>>(invoices);
            return Ok(new Pagination<BookingDto>(invoiceSpecParams.PageIndex, invoiceSpecParams.PageSize, totalItems, data));
        }

        [HttpGet("{invoicenumber}")]
        public async Task<ActionResult<BookingDto>> GetInvoiceByNumber(string invoicenumber)
        {
            var order = await _invoiceRepo.GetOrderByInvoiceNumber(invoicenumber);
            return Ok(order);
        }

        [HttpPost("add-invoice")]
        public async Task<ActionResult<Booking>> AddInvoice(BookingDto invoiceDto)
        {
            string userid = User.GetUserId();
            invoiceDto.BookingNumber = await GenerateInvoiceNumber();
            
            var invoice = _mapper.Map<Booking>(invoiceDto);
            invoice.UserId = Convert.ToInt32(userid);
            _commonRepo.Add(invoice);
            if (!await _commonRepo.SaveAsync()) return BadRequest("Problem in adding");
            return Ok(invoice);
        }
        private async Task<string> GenerateInvoiceNumber()
        {
            var invoiceNumber = String.Empty;
            var invoice = await _invoiceRepo.GetLastOrder();
            if (invoice == null)
            {
                invoiceNumber = DateTime.Now.ToString("yyMMdd") + "-" + 1;
            }
            else
            {
                invoiceNumber = DateTime.Now.ToString("yyMMdd") + "-" + (invoice.Id + 1);
            }
            return $"CHF{invoiceNumber}";
        }
    }
}
