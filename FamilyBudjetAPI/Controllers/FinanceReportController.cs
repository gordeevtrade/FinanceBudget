using AutoMapper;
using FamilyBudjetAPI.Sevices.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FamilyBudjetAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "Google,User")]
    public class FinanceReportController : ControllerBase
    {
        private readonly IFinanceReportService _financeReportService;
        private readonly IMapper _mapper;

        public FinanceReportController(IFinanceReportService financeReportService, IMapper mapper)
        {
            _financeReportService = financeReportService;
            _mapper = mapper;
        }

        [HttpGet("daily")]
        public ActionResult<DailyReportDto> GetDailyReport(DateTime date)
        {
            try
            {
                var dailyReport = _financeReportService.GetDailyReport(date);
                var dailyReportDto = _mapper.Map<DailyReportDto>(dailyReport);
                return Ok(dailyReportDto);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("period")]
        public ActionResult<PeriodReportDto> GetPeriodReport(DateTime startDate, DateTime endDate)
        {
            try
            {
                var periodReport = _financeReportService.GetPeriodReport(startDate, endDate);
                var periodReportDto = _mapper.Map<PeriodReportDto>(periodReport);

                return Ok(periodReportDto);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}