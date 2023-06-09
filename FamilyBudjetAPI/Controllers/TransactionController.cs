using AutoMapper;
using FamilyBudjetAPI.DTOModels;
using FamilyBudjetAPI.Sevices.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FamilyBudjetAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "Google,User")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly IMapper _mapper;

        public TransactionController(ITransactionService transactionService, IMapper mapper)
        {
            _transactionService = transactionService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<FinanceTransactionDto>> GetTransactions()
        {
            var transactions = _transactionService.GetTransactions();
            var transactionDtos = _mapper.Map<IEnumerable<FinanceTransactionDto>>(transactions);
            return Ok(transactionDtos);
        }

        [HttpPost]
        public ActionResult<FinanceTransactionDto> CreateTransaction(FinanceTransactionDto transactionDto)
        {
            var transaction = _mapper.Map<FinanceTransaction>(transactionDto);
            var createdTransaction = _transactionService.CreateTransaction(transaction);
            var createdTransactionDto = _mapper.Map<FinanceTransactionDto>(createdTransaction);
            return Ok(createdTransactionDto);
        }

        [HttpGet("{id}")]
        public ActionResult<FinanceTransactionDto> GetTransaction(int id)
        {
            try
            {
                var transaction = _transactionService.GetTransaction(id);
                var transactionDto = _mapper.Map<FinanceTransactionDto>(transaction);
                return Ok(transactionDto);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("category/{categoryId}")]
        public ActionResult<IEnumerable<FinanceTransactionDto>> GetTransactionsByCategory(int categoryId)
        {
            try
            {
                var transactions = _transactionService.GetTransactionsByCategory(categoryId);
                var transactionDtos = _mapper.Map<IEnumerable<FinanceTransactionDto>>(transactions);
                return Ok(transactionDtos);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTransaction(int id, FinanceTransactionDto updatedTransactionDto)
        {
            try
            {
                var updatedTransaction = _mapper.Map<FinanceTransaction>(updatedTransactionDto);
                _transactionService.UpdateTransaction(id, updatedTransaction);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTransaction(int id)
        {
            try
            {
                _transactionService.DeleteTransaction(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}