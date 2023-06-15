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
using System.Linq.Expressions;
using StackExchange.Redis;
using Hotel.Shared.Exceptions;


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
            var new_role = await _roleRepository.FindAsync(r => r.Id == userToCreateDTO.Role);

            if (new_role == null)
            {
                throw new NotImplementedException();
            }
            else
            {
                new_user.Role = new_role;

                // hashing password
                var encryptPassword = new_user.Password;

                // change password
                new_user.Password = encryptPassword;

                // create new user
                var new_user_with_role = await _userRepository.CreateAsync(new_user);
                return _mapper.Map<UserToReturnDTO>(new_user);
            }

        }
        public async Task RemoveUserAsync(int userId)
        {
             await _userRepository.DeleteByIDAsync(userId);
        }
        public async Task<IEnumerable<UserToReturnDTO>> SearchUserAsync(string searchOption, string searchContent)
        {
            IEnumerable<User>? result;
            switch (searchOption)
            {
                case "id":
                    {
                        Expression<Func<User, bool>> predicate = user => user.Id.ToString().Contains(searchContent);
                        result = await _userRepository.FindAllAsync(predicate);
                        break;
                    }
                case "name":
                    {
                        Expression<Func<User, bool>> predicate = user => user.FullName.Contains(searchContent);
                        result = await _userRepository.FindAllAsync(predicate);
                        break;
                    }
                case "email":
                    {
                        Expression<Func<User, bool>> predicate = user => user.Email.Contains(searchContent);
                        result = await _userRepository.FindAllAsync(predicate);
                        break;
                    }
                default:
                    {
                        throw new ArgumentException("Invalid search option.", nameof(searchOption));
                    }



            }

            return  _mapper.Map<IEnumerable<UserToReturnDTO>>(result);

        }

        public async Task ChangeUserPassWordAsync(int userID, string newPassWord)
        {
            // this function is only used by admin -> no need to check current password

            var user = await _userRepository.FindAsync(u=>u.Id== userID);
            if (user == null)
            {
                throw new NotImplementedException();
            }
            else
            {
                // hashing new password
                var encryptPassword = newPassWord;

                // change password
                user.Password = encryptPassword;
                await _userRepository.UpdateAsync(user);
               
            }
        }
        public async Task ChangeUserPassWordAsync(int userID,string currentPassWord, string newPassWord)
        {
            var user = await _userRepository.FindAsync(u => u.Id == userID);
            if (user == null)
            {
                throw new NotImplementedException();
            }
            else
            {
                // check password
                var isMatch = true;
                if (!isMatch)
                {
                    throw new NotImplementedException();
                }

                // hashing password
                var encryptPassword = newPassWord;

                // change password
                user.Password = encryptPassword;
                await _userRepository.UpdateAsync(user);

            }
        }

        public async Task<UserToReturnDTO> GetUserByIDAsync(int userId)
        {
            var user = await _userRepository.FindAsync(userId);
            if(user == null)
            {
                throw new NotImplementedException();
            }
            else
            {
                return _mapper.Map<UserToReturnDTO>(user);
            }
        }
    }
}
