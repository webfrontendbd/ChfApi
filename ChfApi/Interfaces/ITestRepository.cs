namespace ChfApi.Interfaces
{
    public interface ITestRepository
    {
        Task<bool> IsProductExists(string productname);
    }
}
