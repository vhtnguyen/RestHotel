using Hotel.BusinessLogic.DTO.Users;
namespace Hotel.BusinessLogic.Services.IServices
{
    public interface IUserService
    {
        Task<(string, UserToReturnDTO)> Login(UserCreateDto userLoginDto);
        Task<List<UserToReturnDTO>> GetUserListAsync();
        Task<UserToReturnDTO> CreateUserAsync(UserToCreateDTO userToCreateDTO);
        Task<IEnumerable<UserToReturnDTO>> SearchUserAsync(string searchOption, string searchContent);
        Task RemoveUserAsync(int userId);

        Task<UserToReturnDTO> GetUserByIDAsync(int userId);

        Task ChangeUserPassWordAsync(int userID, string newPassWord);
        Task ChangeUserPassWordAsync(int userID, string currentPassWord, string newPassWord);
        Task AddRole(UserAddRoleDto role);
    }
}
