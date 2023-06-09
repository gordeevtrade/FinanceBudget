namespace FamilyBudjetAPI
{
    public class DailyReportDto
    {
        public DateTime Date { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalExpenses { get; set; }
        public List<FinanceTransaction> Transactions { get; set; }
    }
}