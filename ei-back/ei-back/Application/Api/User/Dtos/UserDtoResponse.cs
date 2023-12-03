using ei_back.Domain.User;

namespace ei_back.Application.Api.User.Dtos
{
    public class UserDtoResponse
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }

        public UserDtoResponse toDto(UserEntity userEntity)
        {
            return new UserDtoResponse
            {
                Id = userEntity.Id,
                UserName = userEntity.UserName,
                FullName = userEntity.FullName
            };
        }
    }
}
