using ChfApi.Entities;

namespace ChfApi.Specifications
{
    public class DoctorSpecification : BaseSpecification<Doctor>
    {
        public DoctorSpecification(EntitySpecParams doctorSpecParams)
            : base(x =>
            (string.IsNullOrEmpty(doctorSpecParams.Search) || x.DoctorName.ToLower().Contains(doctorSpecParams.Search))
            )
        {
            AddOrderBy(x => x.Id);
            ApplyPaging(doctorSpecParams.PageSize * (doctorSpecParams.PageIndex - 1), doctorSpecParams.PageSize);
            if (!string.IsNullOrEmpty(doctorSpecParams.Sort))
            {
                switch (doctorSpecParams.Sort)
                {
                    case "nameAsc":
                        AddOrderBy(x => x.DoctorName);
                        break;
                    case "nameDesc":
                        AddOrderByDescending(x => x.DoctorName);
                        break;
                    default:
                        AddOrderBy(x => x.Id);
                        break;
                }
            }
        }
    }
}
