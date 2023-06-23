using ChfApi.Entities;

namespace ChfApi.Specifications
{
    public class ExpenseCategoryFilterCountSpecification : BaseSpecification<ExpenseCategory>
    {
        public ExpenseCategoryFilterCountSpecification(EntitySpecParams categorySpecParams)
            : base(x =>
            (string.IsNullOrEmpty(categorySpecParams.Search) || x.CategoryName.ToLower().Contains(categorySpecParams.Search))
            )
        {

        }
    }
}
