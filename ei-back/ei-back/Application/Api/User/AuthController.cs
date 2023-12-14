using ei_back.Application.Api.User.Dtos;
using ei_back.Application.Usecases.User;
using Microsoft.AspNetCore.Mvc;

namespace ei_back.Application.Api.User
{
    [ApiController]
    [Route("api/user/auth")]
    public class AuthController : ControllerBase
    {
        private readonly ISignInUseCase _signInUseCase;

        public AuthController(ISignInUseCase signInUseCase)
        {
            _signInUseCase = signInUseCase;
        }

        [HttpPost]
        [Route("signin")]
        public IActionResult Signin([FromBody] LoginDtoRequest loginDtoRequest)
        {
            if (loginDtoRequest == null) return BadRequest("Invalid client request");

            var token = _signInUseCase.Handler(loginDtoRequest);
            if (token == null) return Unauthorized();
            return Ok(token);
        }
    }
}
