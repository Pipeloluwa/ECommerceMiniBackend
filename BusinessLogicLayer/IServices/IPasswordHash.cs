using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.IServices
{
    public interface IPasswordHash
    {

        public string HashPassword(string password);
        public bool VerifyHashPassword(string password, string hashedPassword);

    }
}
