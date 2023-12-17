using ei_back.Application.Api.User.Dtos;
using ei_back.Application.Usecases.User;
using ei_back.Infrastructure.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ei_back.Application.Api.User
{
    [ApiController]
    [Route("api/user")]
    [Authorize("Bearer")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private ICreateUserUseCase _createUserUseCase;
        private readonly IUnitOfWork _unitOfWork;

        public UserController(ILogger<UserController> logger, ICreateUserUseCase createUserUseCase, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _createUserUseCase = createUserUseCase;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [ProducesResponseType((200), Type = typeof(UserDtoResponse))]
        public IActionResult Create([FromBody] UserDtoRequest userDtoRequest)
        {
            if (userDtoRequest == null) return BadRequest();
            var userDtoResponse = _createUserUseCase.Handler(userDtoRequest);
            _unitOfWork.Commit();

            return Ok(userDtoResponse);
        }
    }
}
