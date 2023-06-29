using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BusinessLogic.DTO.Users
{
    public class PassWordtoChangeDTO
    {
        public required string CurrentPassword { get; set; }
        public required string NewPassWord { get; set; }
        

    }
}
