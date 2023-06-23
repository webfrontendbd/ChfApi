namespace ChfApi.Dtos
{
    public class ExpenseDto
    {
        public int Id { get; set; }
        public string ExpenseDate { get; set; }
        public int ExpenseCategoryId {get;set;}
        public string CategoryName { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public decimal Amount { get; set; }
        public string ExpenseNotes { get; set; }
        public string CreatedDate { get; set; }

    }
}
