using ChfApi.Entities;

namespace ChfApi.Specifications
{
    public class BookingFilterCountSpecification : BaseSpecification<Booking>
    {
        public BookingFilterCountSpecification(EntitySpecParams invoiceSpecParams)
            : base(x =>
            (string.IsNullOrEmpty(invoiceSpecParams.Search) || x.BookingNumber.ToLower().Contains(invoiceSpecParams.Search))
            )
        {

        }
    }
}
