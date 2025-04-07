using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.DTOs.RequestDTO
{
    public class UserSignUpDto
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
