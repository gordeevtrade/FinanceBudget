using Budget.BuisnessLogic.Models;

namespace FamilyBudjetAPI
{
    public class PeriodReport : Report
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}