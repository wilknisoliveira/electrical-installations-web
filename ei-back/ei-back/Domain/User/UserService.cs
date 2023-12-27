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
            string sortDirection,
            int pageSize,
            int page)
        {
            var sort = (!string.IsNullOrWhiteSpace(sortDirection) && !sortDirection.Equals("desc")) ? "asc" : "desc";
            var size = (pageSize < 1) ? 10 : pageSize;
            var offset = page > 0 ? (page - 1) * size : 0;

            string query = @"SELECT * FROM users u WHERE 1 = 1";
            if (!string.IsNullOrWhiteSpace(name)) query = query + $" AND u.user_name like '%{name}% ";
            query += $" ORDER BY u.user_name {sort} LIMIT {size} OFFSET {offset} ";

            string countQuery = @"SELECT COUNT(*) FROM user u WHERE 1 = 1";
            if (!string.IsNullOrWhiteSpace(name)) countQuery = countQuery + $" AND u.user_name like '%{name}% ";

            var users = await _userRepository.FindWithPagedSearchAsync(query);
            int totalResults = await _userRepository.GetCountAsync(countQuery);

            var usersDto = new UserGetDtoResponse().ToDtoList(users);

            return new PagedSearchDto<UserGetDtoResponse>
            {
                CurrentPage = page,
                List = usersDto,
                PageSize = pageSize,
                SortDirections = sort,
                TotalResults = totalResults
            };
        
        }
    }
}
