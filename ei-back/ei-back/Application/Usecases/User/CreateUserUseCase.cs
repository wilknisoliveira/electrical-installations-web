using ei_back.Application.Api.User.Dtos;
using ei_back.Domain.User;

namespace ei_back.Application.Usecases.User
{
    public interface ICreateUserUseCase
    {
        Task<UserDtoResponse> Handler(UserDtoRequest userDtoRequest);
    }

    public class CreateUserUseCase : ICreateUserUseCase
    {
        private readonly IUserService _userService;

        public CreateUserUseCase(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<UserDtoResponse> Handler(UserDtoRequest userDtoRequest)
        {
            var user = userDtoRequest.ToEntity();

            var userResponse = await _userService.CreateAsync(user);

            UserDtoResponse userDtoResponse = new UserDtoResponse();

            return userDtoResponse.toDto(userResponse);
        }
    }
}
