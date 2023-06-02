using Budget.DAL.Repositories.Interfaces;
using FamilyBudjetAPI.Sevices.Interface;

namespace FamilyBudjetAPI.Sevices
{
    public class FinanceReportService : IFinanceReportService
    {
        private IUnitOfWOrk _unitOfWOrk;

        public FinanceReportService(IUnitOfWOrk unitOfWOrk)
        {
            _unitOfWOrk = unitOfWOrk;
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
            return _unitOfWOrk.FinanceTransactionRepository.GetTransactions(startDate, endDate);
        }
    }
}