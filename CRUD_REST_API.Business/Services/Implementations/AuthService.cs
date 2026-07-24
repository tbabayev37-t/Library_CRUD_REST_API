using CRUD_REST_API.Business.DTOs.TokenDto;
using CRUD_REST_API.Business.DTOs.UserDto;
using CRUD_REST_API.Business.Helpers;
using CRUD_REST_API.Business.Services.Abstractions;
using CRUD_REST_API.Core.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace CRUD_REST_API.Business.Services.Implementations
{
    public class AuthService(IJWTService _jWTService) : IAuthService
    {
        private static readonly List<User> _users = new();
        
        public Task RegisterAsync(RegisterDto dto)
        {
            if(_users.Any(x=>x.Username == dto.Username))
            {
                throw new Exception("Bu istifadeci artiq adi movcuddur!");
            }

            string hashPassword = PasswordHasher.HashPassword(dto.Password);

            var user = new User
            {
                ID = Guid.NewGuid(),
                Fullname = dto.Fullname,
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = hashPassword,
                Role = "User"
            };
            _users.Add(user);
            return Task.CompletedTask;
        }
        public async Task<AccessTokenDto> LoginAsync(LoginDto dto)
        {
            var user = _users.FirstOrDefault(u=>u.Username == dto.Username);
            if (user == null) throw new Exception("Istifadeci adi ve ya sifre yanlisdir!");

            bool isPasswordValid = PasswordHasher.VerifyPassword(dto.Password, user.PasswordHash);
            if(!isPasswordValid)
            {
                throw new Exception("Istifadeci adi ve ya sifre yanlisdir!");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
            };

            return _jWTService.CreateAccessToken(claims);
        }
    }
}
