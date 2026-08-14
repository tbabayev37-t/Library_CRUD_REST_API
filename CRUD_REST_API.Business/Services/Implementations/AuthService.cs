using CRUD_REST_API.Business.DTOs.TokenDto;
using CRUD_REST_API.Business.DTOs.UserDto;
using CRUD_REST_API.Business.Helpers;
using CRUD_REST_API.Business.Services.Abstractions;
using CRUD_REST_API.Contexts;
using CRUD_REST_API.Core.Enums;
using CRUD_REST_API.Core.Models;
using CRUD_REST_API.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CRUD_REST_API.Business.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IJWTService _jWTService;

        public AuthService(AppDbContext context, IJWTService jWTService)
        {
            _context = context;
            _jWTService = jWTService;
        }

        public async Task RegisterAsync(RegisterDto dto)
        {
            bool isExist = await _context.Users.AnyAsync(x => x.Username == dto.Username);
            if (isExist)
            {
                throw new ArgumentException("Bu istifadəçi adı artıq mövcuddur!");
            }

            string hashPassword = PasswordHasher.HashPassword(dto.Password);
            var user = new CRUD_REST_API.Core.Models.User
            {
                ID = Guid.NewGuid(),
                Fullname = dto.Fullname,
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = hashPassword,
                Role = UserRole.User
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<AccessTokenDto> LoginAsync(LoginDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == dto.Username);
            if (user == null)
                throw new UnauthorizedAccessException("İstifadəçi adı və ya şifrə yanlışdır!");

            bool isPasswordValid = PasswordHasher.VerifyPassword(dto.Password, user.PasswordHash);
            if (!isPasswordValid)
            {
                throw new UnauthorizedAccessException("İstifadəçi adı və ya şifrə yanlışdır!");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()),
                new Claim(ClaimTypes.Name, user.Username ?? string.Empty),
                new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            return _jWTService.CreateAccessToken(claims);
        }
    }
}