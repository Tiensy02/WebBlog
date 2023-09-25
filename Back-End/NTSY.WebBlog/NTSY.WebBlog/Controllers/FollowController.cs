using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NTSY.WebBlog.Application;
using NTSY.WebBlog.Domain;
using NTSY.WebBlog.Infrastructure;

namespace NTSY.WebBlog.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FollowController : BaseController<FollowInsertDto, Follows, Follows>
    {
        private readonly IFollowsService _followsService;
        public FollowController(IFollowsService baseService, IFollowsService followsService) : base(baseService)
        {
            _followsService = followsService;
        }
        /// <summary>
        /// hàm thực hiện lấy dánh sách người dùng đang follow user có id được cho
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/user-following")]
        public async Task<FollowWithPage> GetUserFlollowed(Guid id, Guid userCurrentID, int page, int pageSize)
        {
            var (result,totalFollow) = await _followsService.GetUserFlollowed(id, userCurrentID, page, pageSize);
            return new FollowWithPage() { Follows = result, TotalFollow = totalFollow };

        }
        /// <summary>
        /// hàm lấy danh sách người dùng mà user có id được chọn đang follow
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/user-followed")] 
        public async Task<FollowWithPage> GetUserFollowing(Guid id,Guid userCurrentID, int page, int pageSize)
        {
            var (result,totalFollow) = await _followsService.GetUserFollowing(id, userCurrentID, page, pageSize);
            return new FollowWithPage() { Follows = result, TotalFollow = totalFollow };
        }
    }
}
