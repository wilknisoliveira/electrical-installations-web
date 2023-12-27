using ei_back.Application.Api.User.Dtos;
using ei_back.Domain.User.Interfaces;
using ei_back.Infrastructure.Context;

namespace ei_back.Application.Usecases.User.Interfaces
{
    public class GetUserUseCase : IGetUserUseCase
    {
        private readonly IUserService _userService;

        public GetUserUseCase(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<PagedSearchDto<UserGetDtoResponse>> Handler(string? name, string sortDirection, int pageSize, int page)
        {
            return await _userService.FindWithPagedSearch(name, sortDirection, pageSize, page);
        }
    }
}
