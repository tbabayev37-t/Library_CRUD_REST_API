using CRUD_REST_API.Business.DTOs.TokenDto;
using CRUD_REST_API.Business.Services.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CRUD_REST_API.Business.Services.Implementations
{
    public class JWTService : IJWTService
    {
        private readonly IConfiguration _configuration;

        // Configuration enjekte (inject) olunur
        public JWTService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public AccessTokenDto CreateAccessToken(List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            string secretKey = jwtSettings["Key"] ?? throw new InvalidOperationException("JWT Key tapılmadı!");
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var expires = DateTime.UtcNow.AddMinutes(60);

            var tokenDescription = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: signingCredentials
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            string token = tokenHandler.WriteToken(tokenDescription);

            return new()
            {
                Token = token,
                ExpiredDate = expires
            };
        }
    }
}