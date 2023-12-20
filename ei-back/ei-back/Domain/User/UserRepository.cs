using ei_back.Domain.Base;
using ei_back.Domain.User.Interfaces;

namespace ei_back.Domain.User
{
    public class UserRepository : GenericRepository<UserEntity>, IUserRepository
    {

        public UserRepository(Infrastructure.Context.AppContext context) : base(context) { }

        public UserEntity ValidateCredentials(string userName, string pass)
        {
            return _context.Users.FirstOrDefault(u => u.UserName == userName && u.Password == pass);
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
