using NTSY.WebBlog.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSY.WebBlog.Application
{
    public interface ICommentService : IBaseService<CommentInsertDto,CommentUpdateDto,Comments>
    {
        /// <summary>
        /// hàm lấy danh sách comment trong 1 bài viết có phân trang để hiển thị cho người dùng
        /// </summary>
        /// <param name="postID">id của bài viết</param>
        /// <param name="page">trang hiện tại của danh sách comment</param>
        /// <param name="pageSize">tổng số bản ghi trên 1 trang trong danh sách comment</param>
        /// <returns></returns>
        Task<(IEnumerable<CommentDto>, int)> GetCommentByPostIDForUI(Guid postID, int page, int pageSize);
        /// <summary>
        /// lấy danh sách comment trả lời của 1 comment
        /// </summary>
        /// <param name="commentParentID"> id của comment cha </param>
        /// <returns></returns>
        Task<IEnumerable<CommentDto>> GetCommentByCommentParentID(Guid commentParentID);

        Task PostCommentRoot(CommentInsertForPostDto comment);
    }
}
