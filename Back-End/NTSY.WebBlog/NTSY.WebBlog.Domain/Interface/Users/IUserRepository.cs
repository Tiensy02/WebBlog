using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSY.WebBlog.Domain
{
    public interface IUserRepository :IBaseRepository<Users>
    {
        /// <summary>
        /// hàm thực hiện lấy user theo email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<Users> GetUserbyEmail(string email);
        /// <summary>
        /// hàm thực hiện lấy user theo tên tài khoản và mật khẩu
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<Users> GetUserByNameAndPassword(Users user);

    }
}
