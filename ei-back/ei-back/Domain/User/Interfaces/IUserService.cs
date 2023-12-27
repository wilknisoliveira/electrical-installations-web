using ei_back.Application.Api.User.Dtos;
using ei_back.Infrastructure.Context;
using System.Security.Cryptography;

namespace ei_back.Domain.User.Interfaces
{
    public interface IUserService
    {
        string ComputeHash(string input, HashAlgorithm hashAlgorithm);
        Task<UserEntity> CreateAsync(UserEntity userEntity);
        Task<PagedSearchDto<UserGetDtoResponse>> FindWithPagedSearch(string name, string sortDirection, int pageSize, int page);
    }
}
