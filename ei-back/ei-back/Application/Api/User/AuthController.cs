using ei_back.Application.Api.User.Dtos;
using ei_back.Domain.User;
using Microsoft.AspNetCore.Mvc;

namespace ei_back.Application.Api.User
{
    [ApiController]
    [Route("api/user/auth")]
    public class AuthController : ControllerBase
    {
        private ILoginService _loginService;

        public AuthController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        [Route("signin")]
        public IActionResult Signin([FromBody] LoginDtoRequest userDtoRequest)
        {
            if (userDtoRequest == null) return BadRequest("Invalid client request");

            var token = _loginService.ValidateCredentials(userDtoRequest);
            if (token == null) return Unauthorized();
            return Ok(token);
        }
    }
}
