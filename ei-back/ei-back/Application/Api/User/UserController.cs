using ei_back.Application.Api.User.Dtos;
using ei_back.Application.Usecases.User.Interfaces;
using ei_back.Infrastructure.Context.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ei_back.Application.Api.User
{
    [ApiController]
    [Route("api/[controller]")]
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
        public async Task<IActionResult> Create([FromBody] UserDtoRequest userDtoRequest, CancellationToken cancellationToken = default)
        {
            if (userDtoRequest == null) return BadRequest();
            var userDtoResponse = await _createUserUseCase.Handler(userDtoRequest);
            await _unitOfWork.CommitAsync(cancellationToken);

            return Ok(userDtoResponse);
        }
    }
}
