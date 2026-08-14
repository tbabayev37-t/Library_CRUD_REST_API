using CRUD_REST_API.Business.DTOs.TokenDto;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace CRUD_REST_API.Business.Services.Abstractions
{
    public interface IJWTService
    {
        AccessTokenDto CreateAccessToken(List<Claim> claims);
    }
}
