using NTSY.WebBlog.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSY.WebBlog.Application
{
    public interface IUserService : IBaseService<UserInsertDto, UserUpdateDto,Users>
    {
        Task<Users> GetUserbyEmail(string email);
        Task<Users> GetUserbyNameAndPassword(UserLoginDto user);
    }
}
