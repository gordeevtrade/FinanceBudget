using FamilyBudjetAPI.Sevices.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FamilyBudjetAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FinanceReportController : ControllerBase
    {
        private readonly IFinanceReportService _financeReportService;

        public FinanceReportController(IFinanceReportService financeReportService)
        {
            _financeReportService = financeReportService;
        }

        [HttpGet("daily")]
        public ActionResult<DailyReport> GetDailyReport(DateTime date)
        {
            try
            {
                var dailyReport = _financeReportService.GetDailyReport(date);
                return Ok(dailyReport);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("period")]
        public ActionResult<PeriodReport> GetPeriodReport(DateTime startDate, DateTime endDate)
        {
            try
            {
                var periodReport = _financeReportService.GetPeriodReport(startDate, endDate);
                return Ok(periodReport);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}