using ei_back.Application.Api.User.Dtos;
using ei_back.Application.Usecases.User.Interfaces;
using ei_back.Infrastructure.Context.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace ei_back.Application.Api.User
{
    [ApiController]
    [Route("api/user/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ISignInUseCase _signInUseCase;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<AuthController> _stringLocalizer;

        public AuthController(ISignInUseCase signInUseCase, IUnitOfWork unitOfWork, IStringLocalizer<AuthController> stringLocalizer)
        {
            _signInUseCase = signInUseCase;
            _unitOfWork = unitOfWork;
            _stringLocalizer = stringLocalizer;
        }

        [HttpPost]
        [ProducesResponseType(typeof(TokenDtoReponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("signin")]
        public IActionResult Signin([FromBody] LoginDtoRequest loginDtoRequest)
        {
            if (loginDtoRequest == null) return BadRequest(_stringLocalizer["AuthInvalidRequest"].Value);

            var token = _signInUseCase.Handler(loginDtoRequest);
            _unitOfWork.Commit();

            if (token == null) return Unauthorized();
            return Ok(token);
        }
    }
}
