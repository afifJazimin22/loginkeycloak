using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KeycloakLoginMicroservice.Model;
using KeycloakLoginMicroservice.Services;
using Microsoft.AspNetCore.Mvc;

namespace KeycloakLoginMicroservice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }


        [HttpPost("")]
        public async Task<IActionResult> Login(UserCredential user){


            try
            {
                
            return Ok(await _loginService.Login(user));
            }
            catch (HttpRequestException e)
            {
                
                return Unauthorized(e.Message);
            }
;        }
    }
}