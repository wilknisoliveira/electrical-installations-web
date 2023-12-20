using ei_back.Domain.User.Interfaces;
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
    }
}
