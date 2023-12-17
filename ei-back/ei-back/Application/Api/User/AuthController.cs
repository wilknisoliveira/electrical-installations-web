using ei_back.Application.Api.User.Dtos;
using ei_back.Application.Usecases.User;
using ei_back.Infrastructure.Context;
using Microsoft.AspNetCore.Mvc;

namespace ei_back.Application.Api.User
{
    [ApiController]
    [Route("api/user/auth")]
    public class AuthController : ControllerBase
    {
        private readonly ISignInUseCase _signInUseCase;
        private readonly IUnitOfWork _unitOfWork;

        public AuthController(ISignInUseCase signInUseCase, IUnitOfWork unitOfWork)
        {
            _signInUseCase = signInUseCase;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Route("signin")]
        public IActionResult Signin([FromBody] LoginDtoRequest loginDtoRequest)
        {
            if (loginDtoRequest == null) return BadRequest("Invalid client request");

            var token = _signInUseCase.Handler(loginDtoRequest);
            _unitOfWork.Commit();

            if (token == null) return Unauthorized();
            return Ok(token);
        }
    }
}
