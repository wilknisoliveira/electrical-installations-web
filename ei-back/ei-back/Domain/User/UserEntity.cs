using ei_back.Domain.Base;
using ei_back.Domain.Role;

namespace ei_back.Domain.User
{
    public class UserEntity : BaseEntity
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public List<RoleEntity> Roles { get; } = new();
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}
