using AutoMapper.Configuration.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BusinessLogic.DTO.Users
{
    public class UserUpdateDto
    {
        public required  int Id { get; set; }
        public required string newPassword { get; set; }
    }
}
