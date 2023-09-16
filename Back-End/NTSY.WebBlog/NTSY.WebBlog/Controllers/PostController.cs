using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NTSY.WebBlog.Application;
using NTSY.WebBlog.Application.Dto.Post;
using NTSY.WebBlog.Domain;
using NTSY.WebBlog.Infrastructure;

namespace NTSY.WebBlog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : BaseController<PostInsertDto, PostUpdateDto, Posts>
    {
        private readonly IPostService _postService;
        public PostController(IPostService baseService, IPostService postService) : base(baseService)
        {
            _postService = postService;
        }
        [HttpGet("{id}/post-content")]
        public async Task<PostModel> GetPostByID(  Guid id)
        {
            var result = await _postService.GetPostByID(id);
            return result;
        }
        [HttpGet("PostList")]
        public async Task<PostWithPage> GetPostForUI( int page, int pageSize)
        {
            var (posts,totalPost) = await _postService.GetPostForUI(page, pageSize);
            return new PostWithPage {Posts=posts, TotalPost = totalPost };
        }
        [HttpGet("Filter")]
        public async Task<IEnumerable<PostModel>> GetPostByFilter(string filter = "")
        {
            var result = await _postService.GetPostByFilter(filter);
            return result;
        }
        [HttpPut("RatingCore")]
        public async Task<IActionResult> UpdateRating(PostUpdateRating postUpdateRating)
        {
            await _postService.UpdateRating(postUpdateRating.RatingCore,postUpdateRating.PostID);
            return StatusCode(StatusCodes.Status200OK);
        }
        [HttpGet("User/{id}")]
        public async Task<PostWithPage> GetPostOfUser(Guid id, int page, int pageSize )
        {
            var (result,totalRecord) = await _postService.GetPostsOfUser(id, page, pageSize);
            return new PostWithPage(){ Posts = result, TotalPost =totalRecord };
        }

    }
}
