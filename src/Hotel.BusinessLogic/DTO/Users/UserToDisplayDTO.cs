using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BusinessLogic.DTO.Users
{
    public class UserToDisplayDTO
    {
        public int StaffID { get; set; } 
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? TelephoneNumber { get; set; }
        public string? Role { get; set; }
        public string? AvatarURL { get; set; }

    }
}
