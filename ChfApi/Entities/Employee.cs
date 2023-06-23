namespace ChfApi.Entities
{
    public class Employee : BaseEntity
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public string Designation { get;set; }
        public string Address { get; set; }
        public bool IsActivated { get; set; }
        public ICollection<Expense> Expenses { get; set; }
    }
}
