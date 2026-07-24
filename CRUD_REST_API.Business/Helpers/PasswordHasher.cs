using System;
using System.Collections.Generic;
using System.Text;
namespace CRUD_REST_API.Business.Helpers
{
    public class PasswordHasher
    {
        //parolu sifreleyen metod
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        //daxil edilen sifre ile bazadaki hashi muqayise eden metod
        public static bool VerifyPassword(string password, string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }

    }
}
