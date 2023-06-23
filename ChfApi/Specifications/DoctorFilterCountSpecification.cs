using ChfApi.Entities;

namespace ChfApi.Specifications
{
    public class DoctorFilterCountSpecification : BaseSpecification<Doctor>
    {
        public DoctorFilterCountSpecification(EntitySpecParams doctorSpecParams)
            : base(x =>
            (string.IsNullOrEmpty(doctorSpecParams.Search) || x.DoctorName.ToLower().Contains(doctorSpecParams.Search))
            )
        {

        }
    }
}
