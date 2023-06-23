using ChfApi.Entities;

namespace ChfApi.Specifications
{
    public class ExpenseSpecification : BaseSpecification<Expense>
    {
        public ExpenseSpecification(EntitySpecParams expenseSpecParams)
            : base(x =>
            (string.IsNullOrEmpty(expenseSpecParams.Search) || x.ExpenseNotes.ToLower().Contains(expenseSpecParams.Search))
            )
        {
            AddInclude(x => x.Employee);
            AddInclude(x => x.Category);
            AddOrderByDescending(x => x.ExpenseDate);
            ApplyPaging(expenseSpecParams.PageSize * (expenseSpecParams.PageIndex - 1), expenseSpecParams.PageSize);
            if (!string.IsNullOrEmpty(expenseSpecParams.Sort))
            {
                switch (expenseSpecParams.Sort)
                {
                    case "nameAsc":
                        AddOrderBy(x => x.ExpenseNotes);
                        break;
                    case "nameDesc":
                        AddOrderByDescending(x => x.ExpenseNotes);
                        break;
                    default:
                        AddOrderBy(x => x.Id);
                        break;
                }
            }
        }
    }
}
