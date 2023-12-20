﻿using ei_back.Domain.User;
using System.Data;
using System.Security.Cryptography;

namespace ei_back.Application.Api.User.Dtos
{
    public class UserDtoRequest
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }

        public UserEntity ToEntity()
        {
            return new UserEntity
            {
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                UserName = this.UserName,
                FullName = this.FullName,
                Password = this.Password
            };
        }
    }
}
