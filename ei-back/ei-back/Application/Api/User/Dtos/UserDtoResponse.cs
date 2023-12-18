﻿using ei_back.Domain.User;

namespace ei_back.Application.Api.User.Dtos
{
    public class UserDtoResponse
    {
        public string UserName { get; set; }
        public string FullName { get; set; }

        public UserDtoResponse toDto(UserEntity userEntity)
        {
            return new UserDtoResponse
            {
                UserName = userEntity.UserName,
                FullName = userEntity.FullName
            };
        }
    }
}
