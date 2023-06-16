using Hotel.BusinessLogic.DTO.Users;
namespace Hotel.BusinessLogic.Services.IServices
{
    public interface IUserService
    {
        Task<List<UserToReturnDTO>> GetUsersAsync();
        Task<UserToReturnDTO> CreateUserAsync(UserToCreateDTO userToCreateDTO);
        Task<IEnumerable<UserToReturnDTO>> SearchUserAsync(string searchOption, string searchContent);
        Task RemoveUserAsync(int userId);
        //Task<UserDTO> GetUserAsync(int userId, CancellationToken cancellationToken = default);
        //Task<UserDTO> AddUserAsync(UserToAddDTO userToAddDTO);
        //Task<UserDTO> UpdateUserAsync(UserToUpdateDTO userToUpdateDTO);
    }
}
