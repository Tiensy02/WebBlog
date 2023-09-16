using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NTSY.WebBlog.Application;
using NTSY.WebBlog.Domain;
using NTSY.WebBlog.Infrastructure;

namespace NTSY.WebBlog
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : BaseController<CommentInsertDto, CommentUpdateDto, Comments>
    {
        private readonly ICommentService _commentService;
        public CommentController(ICommentService baseService, ICommentService commentService) : base(baseService)
        {
            _commentService = commentService;
        }
        [HttpGet("{id}/comment-child")]
        public async Task<IEnumerable<CommentDto>> GetCommentByCommentParentID( Guid id)
        {
            var resutl = await _commentService.GetCommentByCommentParentID(id);
            return resutl;
        }
        [HttpGet("{id}/comment-post")]

        public async Task<CommentWithPageDto> GetCommentByPostIDForUI( Guid id, int page,  int pageSize)
        {
            var (comments, totalComments) = await _commentService.GetCommentByPostIDForUI(id, page, pageSize);
            return new CommentWithPageDto { Comments = comments, TotalComment = totalComments };
             
        }
        [HttpPost("/CommentRoot")]
        public async Task<IActionResult> PostCommentRoot(CommentInsertForPostDto comment)
        {
            await _commentService.PostCommentRoot(comment);
            return StatusCode(StatusCodes.Status201Created);
            
        }
    }
}
