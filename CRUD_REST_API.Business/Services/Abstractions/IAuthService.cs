using CRUD_REST_API.Business.DTOs.TokenDto;
using CRUD_REST_API.Business.DTOs.UserDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD_REST_API.Business.Services.Abstractions
{
    public interface IAuthService
    {
        Task RegisterAsync(RegisterDto dto);
        Task<AccessTokenDto> LoginAsync(LoginDto dto);        
    }
}
