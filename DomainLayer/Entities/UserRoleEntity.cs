using DomainLayer.DTOs.RequestDTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities
{
    public class UserRoleEntity
    {
        public int? RoleId { get; set; }
        public Guid? UserId { get; set; }
        public string? UserRole { get; set; }
    }
}
