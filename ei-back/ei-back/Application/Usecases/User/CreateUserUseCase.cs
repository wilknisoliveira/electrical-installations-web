using AutoMapper;
using ei_back.Application.Api.User.Dtos;
using ei_back.Application.Usecases.User.Interfaces;
using ei_back.Domain.User;
using ei_back.Domain.User.Interfaces;

namespace ei_back.Application.Usecases.User
{
    public class CreateUserUseCase : ICreateUserUseCase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public CreateUserUseCase(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<UserDtoResponse> Handler(UserDtoRequest userDtoRequest)
        {
            var user = _mapper.Map<UserEntity>(userDtoRequest);

            var userResponse = await _userService.CreateAsync(user);

            return _mapper.Map<UserDtoResponse>(userResponse); 
        }
    }
}
