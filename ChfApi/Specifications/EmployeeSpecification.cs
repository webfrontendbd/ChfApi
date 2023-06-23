using ChfApi.Entities;

namespace ChfApi.Specifications
{
    public class EmployeeSpecification : BaseSpecification<Employee>
    {
        public EmployeeSpecification(EntitySpecParams employeeSpecParams)
            : base(x =>
            (string.IsNullOrEmpty(employeeSpecParams.Search) || x.Name.ToLower().Contains(employeeSpecParams.Search))
            )
        {
            AddOrderBy(x => x.Id);
            ApplyPaging(employeeSpecParams.PageSize * (employeeSpecParams.PageIndex - 1), employeeSpecParams.PageSize);
            if (!string.IsNullOrEmpty(employeeSpecParams.Sort))
            {
                switch (employeeSpecParams.Sort)
                {
                    case "nameAsc":
                        AddOrderBy(x => x.Name);
                        break;
                    case "nameDesc":
                        AddOrderByDescending(x => x.Name);
                        break;
                    default:
                        AddOrderBy(x => x.Id);
                        break;
                }
            }
        }
    }
}
