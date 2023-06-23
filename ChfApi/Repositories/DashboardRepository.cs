using Microsoft.EntityFrameworkCore;
using ChfApi.Data;
using ChfApi.Dtos.DashboardDtos;
using ChfApi.Interfaces;

namespace ChfApi.Repositories
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly DataContext _context;

        public DashboardRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<CardDto> GetTopCardInfoForDashboard()
        {
            var cardDto = new CardDto
            {
                DailySalesAmount = await this.getDailySales(),
                TotalSalesAmount = await this.getTotalSales(),
                TotalServices = await this.getTotalServices(),
                TotalExpenses = await this.getTotalExpenses(),
            };

            return cardDto;
        }

        private async Task<decimal> getTotalExpenses()
        {
            var totalExpenses = await _context.Expenses.SumAsync(a => a.Amount);
            return totalExpenses;
        }

        private async Task<int> getTotalServices()
        {
            var totalServices = await _context.Tests.CountAsync();
            return totalServices;
        }

        private async Task<decimal> getTotalSales()
        {
            var totalSales = await _context.Bookings.SumAsync(a => a.TotalPayable);
            return totalSales;
        }

        private async Task<decimal> getDailySales()
        {
            var dailySales = await _context.Bookings.Where(d => d.BookingDate.Date == DateTime.Now.Date).SumAsync(a => a.TotalPayable);
            return dailySales;
        }

        public async Task<List<SaleExpenseChartDto>> GeSaleExpenseChartDataForDashboard()
        {
            List<SaleExpenseChartDto> listData = new List<SaleExpenseChartDto>();
            SaleExpenseChartDto salesData = await this.getSalesData();
            listData.Add(salesData);
            SaleExpenseChartDto expenseData = await this.getExpensesData();
            listData.Add(expenseData);
            return listData;
        }

        private async Task<SaleExpenseChartDto> getSalesData()
        {
            int[] dataArr = new int[31];

            var salesDatas = await _context.Bookings
                .GroupBy(g => g.BookingDate.Date)
                .Select(x => new { date = x.Key.Date, amount = x.Sum(a => a.TotalAmount) })
                .ToListAsync();

            if (salesDatas.Count > 0) {
                for (int i = 0; i < salesDatas.Count; i++)
                {
                    dataArr[i] = Convert.ToInt32(salesDatas[i].amount);
                }
            }

            return new SaleExpenseChartDto
            {
                Name = "Sales",
                data = dataArr
            };

        }

        private async Task<SaleExpenseChartDto> getExpensesData()
        {
            int[] dataArr = new int[31];

            var expenseDatas = await _context.Expenses
                .GroupBy(g => g.ExpenseDate.Date)
                .Select(x => new { date = x.Key.Date, amount = x.Sum(a => a.Amount) })
                .ToListAsync();

            if (expenseDatas.Count > 0)
            {
                for (int i = 0; i < expenseDatas.Count; i++)
                {
                    dataArr[i] = Convert.ToInt32(expenseDatas[i].amount);
                }
            }

            return new SaleExpenseChartDto
            {
                Name = "Expenses",
                data = dataArr
            };

        }

    }
}
