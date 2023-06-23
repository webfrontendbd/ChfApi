using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ChfApi.Interfaces;
using System.Net.Mime;

namespace ChfApi.Controllers
{
    [Authorize]
    public class ReportsController : BaseApiController
    {
        private readonly IReportServices _reportServices;

        public ReportsController(IReportServices reportServices)
        {
            _reportServices = reportServices;
        }
        [HttpGet("pos-receipt-mini")]
        public async Task<ActionResult> GetPosReceipt(string invoicenumber)
        {
            byte[] data = await _reportServices.GetBookingPrinterReceiptAsync(invoicenumber);
            return File(data, MediaTypeNames.Application.Pdf, "Booking_Receipt.pdf");
        }
    }
}
