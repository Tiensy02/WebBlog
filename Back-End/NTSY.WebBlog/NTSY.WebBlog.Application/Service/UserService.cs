using AutoMapper;
using NTSY.WebBlog.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSY.WebBlog.Application
{
    public class UserService : BaseService<UserInsertDto, UserUpdateDto, Users>, IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository, IMapper mapper) : base(userRepository, mapper)
        {
            _userRepository = userRepository;
        }

        public async Task<Users> GetUserbyEmail(string email)
        {
            
            var result = await _userRepository.GetUserbyEmail(email);
            return result;
        }

        public async Task<Users> GetUserbyNameAndPassword(UserLoginDto user)
        {
            var userEniity = _mapper.Map<Users>(user);
            var result = await _userRepository.GetUserByNameAndPassword(userEniity);
            return result;
        }
    }
}
