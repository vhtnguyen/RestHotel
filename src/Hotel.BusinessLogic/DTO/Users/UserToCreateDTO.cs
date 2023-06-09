using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BusinessLogic.DTO.Users
{
    public class UserToCreateDTO
    {
        public string? Account { get; set; }
        public string? Password { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? TelephoneNumber { get; set; }
        public int? Role { get; set; }

    }
}
