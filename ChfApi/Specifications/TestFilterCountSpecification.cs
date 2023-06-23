using ChfApi.Entities;
using ChfApi.Specifications.EntityParams;

namespace ChfApi.Specifications
{
    public class TestFilterCountSpecification: BaseSpecification<Test>
    {
        public TestFilterCountSpecification(ProductParams productSpecParams)
            : base(x =>
            (string.IsNullOrEmpty(productSpecParams.Search) || x.Name.ToLower().Contains(productSpecParams.Search)) &&
            (!productSpecParams.CategoryId.HasValue || x.CategoryId == productSpecParams.CategoryId)
            )
        {

        }
    }
}
