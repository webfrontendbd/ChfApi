namespace ChfApi.Dtos
{
    public class ExpenseCategoryDto
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string Code { get; set; }
        public string CreatedDate { get; set; }
        public ICollection<ExpenseDto> Expenses { get; set; }
    }
}
