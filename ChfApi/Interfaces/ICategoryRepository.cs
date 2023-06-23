namespace ChfApi.Interfaces
{
    public interface ICategoryRepository
    {
        Task<bool> IsCategoryExists(string categoryName);
    }
}
