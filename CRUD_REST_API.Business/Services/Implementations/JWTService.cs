using CRUD_REST_API.Business.DTOs.TokenDto;
using CRUD_REST_API.Business.Services.Abstractions;
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
        public AccessTokenDto CreateAccessToken(List<Claim> claims)
        {
            string secretKey = "GizliKeyGizliKeyGizliKeyGizliKeyGizliKeyGizliKeyGizliKeyGizliKey!";
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var signingCredentials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);

            var expires = DateTime.UtcNow.AddMinutes(60);

            var tokenDescription = new JwtSecurityToken(
                issuer: "localhost",
                audience: "localhost",
                claims: claims,
                notBefore: expires
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
