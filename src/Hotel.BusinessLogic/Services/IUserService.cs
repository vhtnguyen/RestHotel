using Hotel.BusinessLogic.DTO.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BusinessLogic.Services
{
    public interface IUserService
    {
        Task<List<UserToDisplayDTO>> GetUsersAsync();
        //Task<UserDTO> GetUserAsync(int userId, CancellationToken cancellationToken = default);
        //Task<UserDTO> AddUserAsync(UserToAddDTO userToAddDTO);
        //Task<UserDTO> UpdateUserAsync(UserToUpdateDTO userToUpdateDTO);
        //Task DeleteUserAsync(int userId);
    }
}
    