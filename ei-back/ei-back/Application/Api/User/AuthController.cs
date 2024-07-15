using ei_back.Application.Api.FunTranslate;
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
        private readonly ILogger<FunTranslateController> _logger;


        public AuthController(ISignInUseCase signInUseCase, IUnitOfWork unitOfWork, IStringLocalizer<AuthController> stringLocalizer, ILogger<FunTranslateController> logger)
        {
            _signInUseCase = signInUseCase;
            _unitOfWork = unitOfWork;
            _stringLocalizer = stringLocalizer;
            _logger = logger;
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

            _logger.LogInformation("API: Logged user - " + loginDtoRequest.UserName);

            return Ok(token);
        }
    }
}
