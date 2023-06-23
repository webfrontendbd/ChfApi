using ChfApi.Entities;

namespace ChfApi.Specifications
{
    public class CategorySpecification : BaseSpecification<Category>
    {
        public CategorySpecification(EntitySpecParams categorySpecParams)
            : base(x =>
            (string.IsNullOrEmpty(categorySpecParams.Search) || x.CategoryName.ToLower().Contains(categorySpecParams.Search))
            )
        {
            AddOrderBy(x => x.Id);
            ApplyPaging(categorySpecParams.PageSize * (categorySpecParams.PageIndex - 1), categorySpecParams.PageSize);
            if (!string.IsNullOrEmpty(categorySpecParams.Sort))
            {
                switch (categorySpecParams.Sort)
                {
                    case "nameAsc":
                        AddOrderBy(x => x.CategoryName);
                        break;
                    case "nameDesc":
                        AddOrderByDescending(x => x.CategoryName);
                        break;
                    default:
                        AddOrderBy(x => x.Id);
                        break;
                }
            }
        }
    }
}
