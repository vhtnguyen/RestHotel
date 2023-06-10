using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.BusinessLogic.DTO.Users;
using AutoMapper;
using Hotel.DataAccess.Entities;
using Hotel.BusinessLogic.Profiles;
using Hotel.DataAccess.Repositories;

namespace Hotel.BusinessLogic.Services
{
    public class UserService: IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public UserService(IUserRepository userRepository, IMapper mapper, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _roleRepository = roleRepository;
        }
        public async Task<List<UserToReturnDTO>> GetUsersAsync()
        {
            var usersToReturn = await _userRepository.GetListAsync();
            return _mapper.Map<List<UserToReturnDTO>>(usersToReturn);
        }
        public async Task<UserToReturnDTO> CreateUserAsync(UserToCreateDTO userToCreateDTO)
        {
            var new_user = _mapper.Map<User>(userToCreateDTO);
            List<Role>? new_roles = await _roleRepository.FindAllAsync(r => r.Id >= userToCreateDTO.Role);
            
            if(new_roles != null) 
            { 
                foreach(var role in new_roles)
                {
                    new_user.AddRole(role);
                }
                var new_user_with_role = await _userRepository.CreateAsync(new_user);
                return _mapper.Map<UserToReturnDTO>(new_user_with_role);
            }
            else
            {
                throw new NotImplementedException();
            }

        }
        public async Task RemoveUserAsync(int userId)
        {
             await _userRepository.DeleteByIDAsync(userId);
        }

    }
}
