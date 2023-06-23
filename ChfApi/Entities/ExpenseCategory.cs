namespace ChfApi.Entities
{
    public class ExpenseCategory : BaseEntity
    {
        public string CategoryName { get; set; }
        public string Code { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;
        public ICollection<Expense> Expenses { get; set; }
    }
}
