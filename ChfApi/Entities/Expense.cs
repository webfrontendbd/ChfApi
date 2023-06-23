namespace ChfApi.Entities
{
    public class Expense : BaseEntity
    {
        public DateTime ExpenseDate { get; set; }
        public int ExpenseCategoryId {get;set;}
        public ExpenseCategory Category { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public decimal Amount { get; set; }
        public string ExpenseNotes { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;

    }
}
