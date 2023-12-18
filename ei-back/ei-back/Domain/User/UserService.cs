using System.Security.Cryptography;
using System.Text;

namespace ei_back.Domain.User
{
    public interface IUserService
    {
        string ComputeHash(string input, HashAlgorithm hashAlgorithm);
        Task<UserEntity> CreateAsync(UserEntity userEntity);
    }
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public string ComputeHash(string input, HashAlgorithm hashAlgorithm)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            Byte[] hashedBytes = hashAlgorithm.ComputeHash(inputBytes);

            return BitConverter.ToString(hashedBytes);
        }

        public async Task<UserEntity> CreateAsync(UserEntity userEntity)
        {
            userEntity.Password = ComputeHash(userEntity.Password, SHA256.Create());
            return await _userRepository.CreateAsync(userEntity);
        }
    }
}
