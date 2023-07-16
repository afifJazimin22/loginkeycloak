using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KeycloakLoginMicroservice.Services;
using Microsoft.AspNetCore.Mvc;

namespace KeycloakLoginMicroservice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LogoutController : ControllerBase
    {
        private readonly ILogoutService _logoutService;

        public LogoutController(ILogoutService logoutService)
        {
            _logoutService = logoutService;
        }

        [HttpPost("/api/logout/{refreshToken}")]
        public async Task<IActionResult> Logout(string refreshToken)
        {
            try
            {
                return Ok(await _logoutService.Logout(refreshToken));
            }
            catch (HttpRequestException e)
            {
                
                return BadRequest(e.Message);
            }
        }
    }
}