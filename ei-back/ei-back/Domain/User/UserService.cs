using ei_back.Application.Api.User.Dtos;
using ei_back.Domain.User.Interfaces;
using ei_back.Infrastructure.Context;
using System.Security.Cryptography;
using System.Text;

namespace ei_back.Domain.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public string ComputeHash(string input, HashAlgorithm hashAlgorithm)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashedBytes = hashAlgorithm.ComputeHash(inputBytes);

            return BitConverter.ToString(hashedBytes);
        }

        public async Task<UserEntity> CreateAsync(UserEntity userEntity)
        {
            userEntity.Password = ComputeHash(userEntity.Password, SHA256.Create());
            return await _userRepository.CreateAsync(userEntity);
            
        }

        public async Task<PagedSearchDto<UserGetDtoResponse>> FindWithPagedSearch(
            string name,
            string sort,
            int size,
            int offset,
            int page)
        {
            var users = await _userRepository.FindWithPagedSearchAsync(
                sort,
                size,
                page,
                offset,
                name,
                "user_name",
                "users");

            int totalResults = await _userRepository.GetCountAsync(
                name,
                "user_name",
                "users");

            return new PagedSearchDto<UserGetDtoResponse>
            {
                CurrentPage = page,
                List = new UserGetDtoResponse().ToDtoList(users),
                PageSize = size,
                SortDirections = sort,
                TotalResults = totalResults
            };
        
        }
    }
}
