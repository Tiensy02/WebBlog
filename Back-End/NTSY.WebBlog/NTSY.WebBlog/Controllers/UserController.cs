using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NTSY.WebBlog.Application;
using NTSY.WebBlog.Domain;
using NTSY.WebBlog.Infrastructure;

namespace NTSY.WebBlog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController<UserInsertDto, UserUpdateDto, Users>
    {
        private readonly IUserService _userSerice;
        public UserController(IUserService baseService) : base(baseService)
        {
            _userSerice = baseService;
        }
        [HttpGet("{email}/Users")]
        public async Task<Users> GetUserbyEmail( string email)
        {
            var result = await _userSerice.GetUserbyEmail(email);
            return result;
        }
        [HttpGet("UserName")]
        public async Task<Users> GetUserByNameAndPassword(String userName , String userPassword)
        {
            var userLogin = new UserLoginDto()
            {
                UserName = userName,
                UserPassword = userPassword
            };
            var result = await _userSerice.GetUserbyNameAndPassword(userLogin);
            return result;
        }
    }
}
