using ei_back.Application.Api.User.Dtos;
using ei_back.Domain.Base;
using ei_back.Infrastructure.Context;
using System.Security.Cryptography;
using System.Text;

namespace ei_back.Domain.User
{
    public interface IUserRepository : IRepository<UserEntity>
    {
        UserEntity ValidateCredentials(LoginDtoRequest userDtoRequest);
        UserEntity RefreshUserInfo(UserEntity user);
    }

    public class UserRepository : GenericRepository<UserEntity>, IUserRepository
    {

        public UserRepository(PostgresContext context) : base(context) { }

        public UserEntity ValidateCredentials(LoginDtoRequest userDtoRequest)
        {
            var pass = ComputeHash(userDtoRequest.Password, SHA256.Create());

            return _context.Users.FirstOrDefault(u => (u.UserName == userDtoRequest.UserName) && (u.Password == pass));
        }

        public UserEntity RefreshUserInfo(UserEntity user)
        {
            if (!_context.Users.Any(u => u.Id.Equals(user.Id))) return null;

            var result = _context.Users.SingleOrDefault(p => p.Id.Equals(user.Id));

            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(user);
                    _context.SaveChanges();
                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return result;
        }

        private string ComputeHash(string input, HashAlgorithm hashAlgorithm)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            Byte[] hashedBytes = hashAlgorithm.ComputeHash(inputBytes);

            return BitConverter.ToString(hashedBytes);
        }

    }
}
