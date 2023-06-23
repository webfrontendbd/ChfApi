using ChfApi.Entities;
using ChfApi.Specifications.EntityParams;

namespace ChfApi.Specifications
{
    public class TestSpecification : BaseSpecification<Test>
    {
        public TestSpecification(ProductParams productSpecParams)
            : base(x =>
            (string.IsNullOrEmpty(productSpecParams.Search) || x.Name.ToLower().Contains(productSpecParams.Search)) &&
            (!productSpecParams.CategoryId.HasValue || x.CategoryId == productSpecParams.CategoryId)
            )
        {
            AddInclude(x => x.Category);
            AddOrderBy(x => x.Id);
            ApplyPaging(productSpecParams.PageSize * (productSpecParams.PageIndex - 1), productSpecParams.PageSize);
            if (!string.IsNullOrEmpty(productSpecParams.Sort))
            {
                switch (productSpecParams.Sort)
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
