﻿using ei_back.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ei_back.Domain.User
{
    [Table("users")]
    public class UserEntity : BaseEntity
    {
        [Column("user_name")]
        public string UserName { get; set; }

        [Column("full_name")]
        public string FullName { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("refresh_token")]
        public string? RefreshToken { get; set; }

        [Column("refresh_token_expiry_time", TypeName = "timestamp without time zone")]
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}
