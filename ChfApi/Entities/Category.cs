namespace ChfApi.Entities
{
    public class Category : BaseEntity
    {
        public string CategoryName { get; set; }
        public ICollection<Test> Tests { get; set;}
    }
}
