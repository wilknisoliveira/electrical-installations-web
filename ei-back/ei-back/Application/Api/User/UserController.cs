using ei_back.Application.Api.User.Dtos;
using ei_back.Application.Usecases.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ei_back.Application.Api.User
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private ICreateUserUseCase _createUserUseCase;

        public UserController(ILogger<UserController> logger, ICreateUserUseCase createUserUseCase)
        {
            _logger = logger;
            _createUserUseCase = createUserUseCase;
        }

        [HttpPost]
        [ProducesResponseType((200), Type = typeof(UserDtoResponse))]
        public IActionResult Create([FromBody] UserDtoRequest userDtoRequest)
        {
            if (userDtoRequest == null) return BadRequest();
            return Ok(_createUserUseCase.Handler(userDtoRequest));
        }
    }
}
