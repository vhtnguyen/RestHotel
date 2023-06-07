using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.DataAccess.Repositories;
using Hotel.BusinessLogic.DTO.Users;
using AutoMapper;
using Hotel.DataAccess.Entities;

namespace Hotel.BusinessLogic.Services
{
    public class UserService: IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<List<UserToDisplayDTO>> GetUsersAsync()
        {
            var usersToReturn = await _userRepository.GetListAsync();

            return _mapper.Map<List<UserToDisplayDTO>>(usersToReturn);
        }
    
    }
}
