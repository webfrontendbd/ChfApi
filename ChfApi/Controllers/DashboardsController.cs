using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ChfApi.Dtos.DashboardDtos;
using ChfApi.Interfaces;

namespace ChfApi.Controllers
{
    [Authorize]
    public class DashboardsController : BaseApiController
    {
        private readonly IDashboardRepository _dashRepo;

        public DashboardsController(IDashboardRepository dashRepo)
        {
            _dashRepo = dashRepo;
        }
        [HttpGet("dashboard-top-card-info")]
        public async Task<ActionResult<CardDto>> GetTopCardInfo()
        {
            var cardInfo = await _dashRepo.GetTopCardInfoForDashboard();
            return Ok(cardInfo);
        }

        [HttpGet("dashboard-sale-expense-chart")]
        public async Task<ActionResult<SaleExpenseChartDto>> GetSaleExpenseChartData()
        {
            var chartInfo = await _dashRepo.GeSaleExpenseChartDataForDashboard();
            return Ok(chartInfo);
        }

    }
}
