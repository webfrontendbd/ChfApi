namespace ChfApi.Dtos.DashboardDtos
{
    public class ChartData
    {
        public int value { get; set; }
    }
    public class SaleExpenseChartDto
    {
        public string Name { get; set; }
        public int[] data { get; set; }
    }
}
