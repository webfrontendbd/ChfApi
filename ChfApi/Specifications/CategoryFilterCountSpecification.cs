using ChfApi.Entities;

namespace ChfApi.Specifications
{
    public class CategoryFilterCountSpecification : BaseSpecification<Category>
    {
        public CategoryFilterCountSpecification(EntitySpecParams categorySpecParams)
            : base(x =>
            (string.IsNullOrEmpty(categorySpecParams.Search) || x.CategoryName.ToLower().Contains(categorySpecParams.Search))
            )
        {

        }
    }
}
