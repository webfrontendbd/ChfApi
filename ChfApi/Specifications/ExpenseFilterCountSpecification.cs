using ChfApi.Entities;

namespace ChfApi.Specifications
{
    public class ExpenseFilterCountSpecification : BaseSpecification<Expense>
    {
        public ExpenseFilterCountSpecification(EntitySpecParams expenseSpecParams)
            : base(x =>
            (string.IsNullOrEmpty(expenseSpecParams.Search) || x.ExpenseNotes.ToLower().Contains(expenseSpecParams.Search))
            )
        {

        }
    }
}
