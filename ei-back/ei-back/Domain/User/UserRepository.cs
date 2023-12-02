using ei_back.Application.Api.User.Dtos;
using ei_back.Infrastructure.Context;
using System.Security.Cryptography;
using System.Text;

namespace ei_back.Domain.User
{
    public interface IUserRepository
    {
        User ValidateCredentials(UserDtoRequest userDtoRequest);
        User RefreshUserInfo(User user);
    }

    public class UserRepository : IUserRepository
    {
        private readonly PostgresContext _context;

        public UserRepository(PostgresContext context)
        {
            _context = context;
        }

        public User ValidateCredentials(UserDtoRequest userDtoRequest)
        {
            var pass = ComputeHash(userDtoRequest.Password, SHA256.Create());

            return _context.Users.FirstOrDefault(u => (u.UserName == userDtoRequest.UserName) && (u.Password == pass));
        }

        public User RefreshUserInfo(User user)
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
