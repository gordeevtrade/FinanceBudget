namespace FamilyBudjetAPI.Sevices.Interface
{
    public interface ITransactionService
    {
        IEnumerable<FinanceTransaction> GetTransactions();

        FinanceTransaction GetTransaction(int id);

        FinanceTransaction CreateTransaction(FinanceTransaction transaction);

        void UpdateTransaction(int id, FinanceTransaction updatedTransaction);

        void DeleteTransaction(int id);

        IEnumerable<FinanceTransaction> GetTransactionsByCategory(int categoryId);
    }
}