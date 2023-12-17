using ei_back.Application.Api.User.Dtos;
using ei_back.Domain.Base;
using ei_back.Infrastructure.Context;
using System.Security.Cryptography;
using System.Text;

namespace ei_back.Domain.User
{
    public interface IUserRepository : IRepository<UserEntity>
    {
        UserEntity ValidateCredentials(string userName, string pass);
        UserEntity RefreshUserInfo(UserEntity user);
    }

    public class UserRepository : GenericRepository<UserEntity>, IUserRepository
    {

        public UserRepository(PostgresContext context) : base(context) { }

        public UserEntity ValidateCredentials(string userName, string pass)
        {
            return _context.Users.FirstOrDefault(u => (u.UserName == userName) && (u.Password == pass));
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
                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return result;
        }

    }
}
