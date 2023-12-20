using System.Security.Cryptography;

namespace ei_back.Domain.User.Interfaces
{
    public interface IUserService
    {
        string ComputeHash(string input, HashAlgorithm hashAlgorithm);
        Task<UserEntity> CreateAsync(UserEntity userEntity);
    }
}
