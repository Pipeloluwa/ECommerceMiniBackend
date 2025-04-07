using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;
using BusinessLogicLayer.IServices;

namespace BusinessLogicLayer.Services
{
    public class PasswordHash:IPasswordHash
    {

        public string HashPassword(string password) { 
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            return hashedPassword;
        }


        public bool VerifyHashPassword(string password, string hashedPassword)
        {
            bool isMatch = BCrypt.Net.BCrypt.Verify(password, hashedPassword);
            return isMatch;
        }


    }
}
