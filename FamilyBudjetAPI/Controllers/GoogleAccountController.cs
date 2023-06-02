using Budget.BuisnessLogic.Sevices.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FamilyBudjetAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GoogleAccountController : ControllerBase
    {
        private IGoogleAuthService _googleAuthService;

        public GoogleAccountController(IGoogleAuthService authService)
        {
            _googleAuthService = authService;
        }

        [HttpGet("googleAuthenticate")]
        public IActionResult Login()
        {
            return Redirect(_googleAuthService.GetAuthUrl());
        }

        [HttpGet("xx")]
        public IActionResult Login2()
        {
            return Ok("dfsfs");
        }

        [HttpGet]
        [Route("callback")]
        public async Task<ActionResult<string>> ReturnGoogleToken(string code)
        {
            try
            {
                return Ok(await _googleAuthService.GetToken(code));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}