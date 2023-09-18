using AutoMapper;
using NTSY.WebBlog.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSY.WebBlog.Application
{
    public class CommentsService : BaseService<CommentInsertDto, CommentUpdateDto, Comments>, ICommentService
    {
        private readonly ICommentRepository _commentRepository;
      
        public CommentsService(ICommentRepository commentRepository, IMapper mapper) 
            : base(commentRepository, mapper)
        {
            _commentRepository = commentRepository;
        }
        /// <summary>
        /// hàm lấy tất cả comment con của 1 comment 
        /// </summary>
        /// <param name="commentParentID"></param>
        /// <returns></returns>
        public async Task<IEnumerable<CommentDto>> GetCommentByCommentParentID(Guid commentParentID)
        {
            var resutl = await _commentRepository.GetCommentByCommentParentID(commentParentID);
            var result1 = _mapper.Map<IEnumerable<CommentDto>>(resutl);
            return result1;
        }
        /// <summary>
        /// hàm lấy tất cả comment của 1 bài viết ( comment ko có comment cha )
        /// </summary>
        /// <param name="postID"></param>
        /// <returns></returns>
        public async Task<IEnumerable<CommentDto>> GetCommentByPostIDForUI(Guid postID)
        {
            var result = await _commentRepository.GetCommentByPostIDForUI(postID);
            var result1 = _mapper.Map<IEnumerable<CommentDto>>(result);
            return result1;
        }

        public async Task<IEnumerable<CommentDto>> GetNewComment(int numberComment)
        {
            var comments = await _commentRepository.GetNewComment(numberComment);
            var result = _mapper.Map<IEnumerable<CommentDto>>(comments);
            return result;
        }

        public async Task PostCommentRoot(CommentInsertForPostDto commentDto)
        {
            var comment = _mapper.Map<Comments>(commentDto);
            await _commentRepository.PostCommentRoot(comment);

        }
    }
}
