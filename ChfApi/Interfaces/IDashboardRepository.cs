using ChfApi.Dtos.DashboardDtos;

namespace ChfApi.Interfaces
{
    public interface IDashboardRepository
    {
        Task<CardDto> GetTopCardInfoForDashboard();
        Task<List<SaleExpenseChartDto>> GeSaleExpenseChartDataForDashboard();
    }
}
