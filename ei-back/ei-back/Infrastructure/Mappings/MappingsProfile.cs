using AutoMapper;
using ei_back.Application.Api.User.Dtos;
using ei_back.Domain.User;

namespace ei_back.Infrastructure.Mappings
{
    public class MappingsProfile : Profile
    {
        public MappingsProfile()
        {
            CreateMap<UserEntity, UserDtoResponse>();
            CreateMap<UserEntity, UserGetDtoResponse>();
            CreateMap<UserDtoRequest, UserEntity>();
        }
    }
}
