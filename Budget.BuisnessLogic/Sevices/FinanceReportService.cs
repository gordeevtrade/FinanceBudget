using FamilyBudjetAPI.Sevices.Interface;

namespace FamilyBudjetAPI.Sevices
{
    public class FinanceReportService : IFinanceReportService
    {
        private readonly FinanceContext _context;

        public FinanceReportService(FinanceContext context)
        {
            _context = context;
        }

        public DailyReport GetDailyReport(DateTime date)
        {
            var transactions = GetTransactions(date, date);
            if (transactions.Count == 0)
            {
                throw new Exception("Date Report Not Found");
            }

            decimal totalIncome = transactions
                .Where(t => t.Amount > 0)
                .Sum(t => t.Amount);

            decimal totalExpenses = transactions
                .Where(t => t.Amount < 0)
                .Sum(t => t.Amount);

            var dailyReport = new DailyReport
            {
                Date = date,
                TotalIncome = totalIncome,
                TotalExpenses = totalExpenses,
                Transactions = transactions
            };

            return dailyReport;
        }

        public PeriodReport GetPeriodReport(DateTime startDate, DateTime endDate)
        {
            var transactions = GetTransactions(startDate, endDate);
            if (transactions.Count == 0)
            {
                throw new Exception("Date Peridod not found");
            }

            decimal totalIncome = transactions
                .Where(t => t.Amount > 0)
                .Sum(t => t.Amount);

            decimal totalExpenses = transactions
                .Where(t => t.Amount < 0)
                .Sum(t => t.Amount);

            var periodReport = new PeriodReport
            {
                StartDate = startDate,
                EndDate = endDate,
                TotalIncome = totalIncome,
                TotalExpenses = totalExpenses,
                Transactions = transactions
            };

            return periodReport;
        }

        private List<FinanceTransaction> GetTransactions(DateTime startDate, DateTime endDate)
        {
            return _context.FinacneTransactions
                .Where(t => t.Date.Date >= startDate.Date && t.Date.Date <= endDate.Date)
                .ToList();
        }
    }
}