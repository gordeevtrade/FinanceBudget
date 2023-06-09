namespace FamilyBudjetAPI.DTOModels
{
    public class FinanceTransactionDto
    {
        public decimal Amount { get; set; }

        public DateTime Date { get; set; }

        public string Note { get; set; }

        public int CategoryId { get; set; }
    }
}