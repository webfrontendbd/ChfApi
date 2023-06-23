using ChfApi.Entities;

namespace ChfApi.Specifications
{
    public class EmployeeFilterCountSpecification : BaseSpecification<Employee>
    {
        public EmployeeFilterCountSpecification(EntitySpecParams employeeSpecParams)
            : base(x =>
            (string.IsNullOrEmpty(employeeSpecParams.Search) || x.Name.ToLower().Contains(employeeSpecParams.Search))
            )
        {

        }
    }
}
