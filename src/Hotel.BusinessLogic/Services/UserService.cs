using Hotel.BusinessLogic.DTO.Users;
using AutoMapper;
using Hotel.DataAccess.Entities;
using System.Linq.Expressions;
using Hotel.Shared.Exceptions;
using Hotel.BusinessLogic.Services.IServices;
using Hotel.DataAccess.Repositories.IRepositories;
using Hotel.Shared.Authentication;

namespace Hotel.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IStringHasher _stringHasher;
        private readonly ITokenGenerator _tokenGenerator;

        public UserService(
            ITokenGenerator tokenGenerator,
            IUserRepository userRepository,
            IMapper mapper,
            IRoleRepository roleRepository,
            IStringHasher stringHasher)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _roleRepository = roleRepository;
            _stringHasher = stringHasher;
            _tokenGenerator = tokenGenerator;
        }
        public async Task<List<UserToReturnDTO>> GetUserListAsync()
        {
            var usersToReturn = await _userRepository.GetListAsync();
            return _mapper.Map<List<UserToReturnDTO>>(usersToReturn);
        }
        public async Task<UserToReturnDTO> GetUserByIDAsync(int userId)
        {
            var user = await _userRepository.FindAsync(userId);
            if (user == null)
            {
                throw new DomainBadRequestException("", "User not found.");
            }
            else
            {
                return _mapper.Map<UserToReturnDTO>(user);
            }
        }

        public async Task<UserToReturnDTO> GetUserByUsernameAsync(string username)
        {
            var user = await _userRepository.FindAsync(u=>u.Account== username);
            if (user == null)
            {
                throw new DomainBadRequestException("", "User not found");

            }
            else
            {
                return _mapper.Map<UserToReturnDTO>(user);
            }
        }

        public async Task<UserToReturnDTO> CreateUserAsync(UserToCreateDTO userToCreateDTO)
        {
            var user = await _userRepository.FindAsync(u => u.Account == userToCreateDTO.Account);
            if (user != null)
            {
                throw new DomainBadRequestException("Create fail", "Username is exist");
            }
            var new_role = await _roleRepository.FindAsync(r => r.Id == userToCreateDTO.Role);

            if (new_role == null)
            {
                throw new DomainBadRequestException("Create fail", "Role not found");
            }
            var new_user = _mapper.Map<User>(userToCreateDTO);
            // change password
            new_user.Password = _stringHasher.Hash(new_user.Password!);

            // add role
            new_user.Role = new_role;
            // create new user
            var new_user_with_role = await _userRepository.CreateAsync(new_user);
            return _mapper.Map<UserToReturnDTO>(new_user);
        }
        public async Task RemoveUserAsync(int userId)
        {
            var user = _userRepository.FindAsync(userId);
            if (user == null)
            {
                throw new DomainBadRequestException("Remove fail", "User not found");
            }
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

            return _mapper.Map<IEnumerable<UserToReturnDTO>>(result);

        }

        public async Task ChangeUserPassWordAsync(int userID, string newPassWord)
        {
            // this function is only used by admin -> no need to check current password

            var user = await _userRepository.FindAsync(u => u.Id == userID);
            if (user == null)
            {
                throw new DomainBadRequestException("Change fail", "User not found");
            }
            else
            {
                // hashing new password
                var encryptPassword = _stringHasher.Hash(newPassWord); ;

                // change password
                user.Password = encryptPassword;
                await _userRepository.UpdateAsync(user);

            }
        }
        public async Task ChangeUserPassWordAsync(int userID, string currentPassWord, string newPassWord)
        {
            var user = await _userRepository.FindAsync(u => u.Id == userID);
            if (user == null)
            {
                throw new DomainBadRequestException("Change fail", "User not found");
            }
            else
            {
                // check password
                var isMatch = _stringHasher.Verify(user.Password!, currentPassWord);
                if (!isMatch)
                {
                    throw new DomainBadRequestException("Change fail", "Wrong password");
                }

                // hashing password
                var encryptPassword = _stringHasher.Hash(newPassWord);

                // change password
                user.Password = encryptPassword;
                await _userRepository.UpdateAsync(user);

            }
        }

        public async Task<(string, UserToReturnDTO)> Login(UserCreateDto userLoginDto)
        {
            var user = await _userRepository.FindAsync(u => u.Account == userLoginDto.Username);
            if (user == null)
            {
                throw new DomainBadRequestException("Login fail", "Invalid user");
            }

            var isMatch = _stringHasher.Verify(user.Password!, userLoginDto.Password);
            if (isMatch == false)
            {
                throw new DomainBadRequestException("Login fail", "Invalid user");
            }

            var roles = new List<string>();
            roles.Add(user.Role!.NameType!);
            var tokenPayload = new TokenPayload
            {
                Id = user.Id,
                Username = user.Account!,
                Roles = roles
            };
            var token = await _tokenGenerator.GenerateToken(tokenPayload);

            return (token, _mapper.Map<UserToReturnDTO>(user));
        }
    }
}
