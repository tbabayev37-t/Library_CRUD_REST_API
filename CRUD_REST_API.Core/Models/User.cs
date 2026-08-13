using CRUD_REST_API.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD_REST_API.Core.Models
{
    public class User
    {
        public Guid ID { get; set; }
        public string Fullname { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public UserRole Role { get; set; } = UserRole.User;
    }
}
