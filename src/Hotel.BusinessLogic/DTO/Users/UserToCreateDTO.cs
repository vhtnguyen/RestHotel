using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BusinessLogic.DTO.Users
{
    public class UserToCreateDTO
    {
        public required string Account { get; set; }
        public required string Password { get; set; }
        public required string FullName { get; set; }
        public string? Email { get; set; }
        public string? TelephoneNumber { get; set; }
        public required int Role { get; set; }

    }
}
