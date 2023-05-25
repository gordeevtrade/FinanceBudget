namespace FamilyBudjetAPI
{
    public class PeriodReport
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalExpenses { get; set; }
        public List<FinanceTransaction> Transactions { get; set; }
    }
}