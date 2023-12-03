using ei_back.Application.Api.User.Dtos;
using ei_back.Domain.User;

namespace ei_back.Application.Usecases.User
{
    public interface ICreateUserUseCase
    {
        UserDtoResponse Handler(UserDtoRequest userDtoRequest);
    }

    public class CreateUserUseCase : ICreateUserUseCase
    {
        private readonly IUserService _userService;

        public CreateUserUseCase(IUserService userService)
        {
            _userService = userService;
        }

        public UserDtoResponse Handler(UserDtoRequest userDtoRequest)
        {
            var user = userDtoRequest.ToEntity();

            var userResponse = _userService.Create(user);

            UserDtoResponse userDtoResponse = new UserDtoResponse();

            return userDtoResponse.toDto(userResponse);
        }
    }
}
