using AspNetCore.Reporting;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ChfApi.Data;
using ChfApi.Dtos;
using ChfApi.Dtos.ReportDtos;
using ChfApi.Interfaces;
using System.Reflection;
using System.Text;
using System.Text.Json;
using CloudinaryDotNet;
using ChfApi.Helpers;

namespace ChfApi.Services
{
    public class ReportServices : IReportServices
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ReportServices(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<byte[]> GetBookingPrinterReceiptAsync(string invoiceNumber)
        {
            List<BookingReceiptDto> receiptData = new List<BookingReceiptDto>();
            string fileDirPath = Assembly.GetExecutingAssembly().Location.Replace("ChfApi.dll", string.Empty);
            string reportFilePath = string.Format("{0}ReportFiles\\booking_invoice.rdlc", fileDirPath);
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding.GetEncoding("utf-8");
            LocalReport report = new LocalReport(reportFilePath);

            var invoice = await GetInvoiceByInvoiceNumber(invoiceNumber);
            string inwords = NumberToWords.ConvertAmountToWords(Convert.ToDouble(invoice.TotalPayable));

            foreach (var item in invoice.BookingDetails)
            {
                BookingReceiptDto obj = new BookingReceiptDto();
                obj.BookingNumber = invoice.BookingNumber;
                obj.BookingDate = invoice.BookingDate;
                obj.DeliveryDate = invoice.DeliveryDate;
                obj.PatientName = invoice.PatientName;
                obj.Age = invoice.Age;
                obj.Gender = invoice.Gender;
                obj.Phone = invoice.Phone;
                obj.Address = invoice.Address;
                obj.BookedBy = invoice.BookedBy;
                obj.Discount = invoice.Discount;
                obj.TotalAmount = invoice.TotalAmount;
                obj.TotalPayable = invoice.TotalPayable;
                obj.DoctorName = invoice.DoctorName;
                obj.Specialist = invoice.Specialist;
                obj.Status = invoice.Status;
                obj.Discount = invoice.Discount;
                obj.TestName = item.TestName;
                obj.Quantity = item.Quantity;
                obj.Price = item.Price;
                obj.InWords = inwords;
                receiptData.Add(obj);
            }
            report.AddDataSource("BookingDataSet", receiptData);

            Dictionary<string, string> parameters = new Dictionary<string, string>();

            var result = report.Execute(RenderType.Pdf, 1, parameters);
            return result.MainStream;
        }

        public async Task<BookingDto> GetInvoiceByInvoiceNumber(string invoiceNumber)
        {
            return await _context.Bookings
               .Where(i => i.BookingNumber == invoiceNumber)
               .ProjectTo<BookingDto>(_mapper.ConfigurationProvider)
               .SingleOrDefaultAsync();
        }

    }
}
