using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD_REST_API.Business.DTOs.TokenDto
{
    public class AccessTokenDto
    {
        ///<summary> AccessTokenDto tokenin ozunu ve bu tokenin ne vaxta qeder kecerli olacagini saxlayir</summary>
        public string Token {  get; set; }=string.Empty;
        public DateTime ExpiredDate { get; set; }
    }
}
