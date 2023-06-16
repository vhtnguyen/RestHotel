using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BusinessLogic.DTO.Users
{
    public class UserToReturnDTO
    {
        public required int Id { get; set; } 
        public required string FullName { get; set; }
        public string? Email { get; set; }
        public string? TelephoneNumber { get; set; }
        public required string Roles { get; set; }
        public string? Avatar { get; set; }

    }
}
