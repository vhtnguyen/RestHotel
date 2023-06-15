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
        Task<List<UserToReturnDTO>> GetUsersAsync();
        Task<UserToReturnDTO>CreateUserAsync(UserToCreateDTO userToCreateDTO);
        Task<IEnumerable<UserToReturnDTO>> SearchUserAsync(string searchOption, string searchContent);
        Task RemoveUserAsync(int userId);

        Task<UserToReturnDTO> GetUserByIDAsync(int userId);
     
        Task ChangeUserPassWordAsync(int userID, string newPassWord);
        Task ChangeUserPassWordAsync(int userID,string currentPassWord,string newPassWord);
        
    }
}
    