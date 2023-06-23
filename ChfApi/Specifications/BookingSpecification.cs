using ChfApi.Entities;

namespace ChfApi.Specifications
{
    public class BookingSpecification : BaseSpecification<Booking>
    {
        public BookingSpecification(EntitySpecParams invoiceSpecParams)
            : base(x =>
            (string.IsNullOrEmpty(invoiceSpecParams.Search) || x.BookingNumber.ToLower().Contains(invoiceSpecParams.Search))
            )
        {
            AddInclude(d => d.BookingDetails);
            AddInclude(p => p.Payments);
            AddInclude(p => p.User);
            AddInclude(p => p.Doctor);
            AddOrderByDescending(x => x.Id);
            ApplyPaging(invoiceSpecParams.PageSize * (invoiceSpecParams.PageIndex - 1), invoiceSpecParams.PageSize);
            if (!string.IsNullOrEmpty(invoiceSpecParams.Sort))
            {
                switch (invoiceSpecParams.Sort)
                {
                    case "nameAsc":
                        AddOrderBy(x => x.BookingNumber);
                        break;
                    case "nameDesc":
                        AddOrderByDescending(x => x.BookingNumber);
                        break;
                    default:
                        AddOrderBy(x => x.Id);
                        break;
                }
            }
        }
    }
}
