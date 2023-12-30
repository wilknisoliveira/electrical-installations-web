using ei_back.Domain.Base;
using ei_back.Domain.User;

namespace ei_back.Domain.Role
{
    public class RoleEntity : BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public List<UserEntity> Users { get; } = new();
    }
}
