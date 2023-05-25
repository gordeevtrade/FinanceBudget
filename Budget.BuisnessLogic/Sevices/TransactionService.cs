using FamilyBudjetAPI.Sevices.Interface;

namespace FamilyBudjetAPI.Sevices
{
    public class TransactionService : ITransactionService
    {
        private readonly FinanceContext _context;

        public TransactionService(FinanceContext context)
        {
            _context = context;
        }

        public IEnumerable<FinanceTransaction> GetTransactions()
        {
            return _context.FinacneTransactions.ToList();
        }

        public FinanceTransaction GetTransaction(int id)
        {
            var transaction = _context.FinacneTransactions.Find(id);

            if (transaction == null)
            {
                throw new Exception("Transaction not found");
            }

            return transaction;
        }

        public FinanceTransaction CreateTransaction(FinanceTransaction transaction)
        {
            _context.FinacneTransactions.Add(transaction);
            _context.SaveChanges();
            return transaction;
        }

        public void UpdateTransaction(int id, FinanceTransaction updatedTransaction)
        {
            var transaction = _context.FinacneTransactions.Find(id);

            if (transaction == null)
            {
                throw new Exception($"Transaction with id {id} not found");
            }

            transaction.Amount = updatedTransaction.Amount;
            transaction.Date = updatedTransaction.Date;
            transaction.Note = updatedTransaction.Note;
            transaction.CategoryId = updatedTransaction.CategoryId;
            _context.Update(transaction);
            _context.SaveChanges();
        }

        public void DeleteTransaction(int id)
        {
            var transaction = _context.FinacneTransactions.Find(id);

            if (transaction == null)
            {
                throw new Exception($"Transaction with id {id} not found");
            }
            _context.FinacneTransactions.Remove(transaction);
            _context.SaveChanges();
        }

        public IEnumerable<FinanceTransaction> GetTransactionsByCategory(int categoryId)
        {
            var transactions = _context.FinacneTransactions
                .Where(t => t.CategoryId == categoryId)
                .ToList();

            if (!transactions.Any())
            {
                throw new Exception("Transactions not found for the provided category");
            }

            return transactions;
        }
    }
}