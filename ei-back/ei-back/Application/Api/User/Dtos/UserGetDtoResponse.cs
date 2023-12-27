using ei_back.Domain.User;

namespace ei_back.Application.Api.User.Dtos
{
    public class UserGetDtoResponse
    {
        public Guid Id {  get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }

        public UserGetDtoResponse ToDto(UserEntity userEntity)
        {
            return new UserGetDtoResponse
            {
                Id = userEntity.Id,
                UserName = userEntity.UserName,
                FullName = userEntity.FullName,
            };
        }

        public List<UserGetDtoResponse> ToDtoList(List<UserEntity> userEntityList)
        {
            return userEntityList.Select(user => ToDto(user)).ToList();
        }
    }
}
