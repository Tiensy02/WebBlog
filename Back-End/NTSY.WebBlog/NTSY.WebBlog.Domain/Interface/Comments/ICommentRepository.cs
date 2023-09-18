using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSY.WebBlog.Domain
{
    public interface ICommentRepository : IBaseRepository<Comments>
    {
        /// <summary>
        /// hàm lấy danh sách comment trong 1 bài viết 
        /// </summary>
        /// <param name="postID">id của bài viết</param>
        /// <returns></returns>
        Task<IEnumerable<CommentModel>> GetCommentByPostIDForUI(Guid postID);
        /// <summary>
        /// lấy danh sách comment trả lời của 1 comment
        /// </summary>
        /// <param name="commentParentID"> id của comment cha </param>
        /// <returns></returns>
        Task<IEnumerable<CommentModel>> GetCommentByCommentParentID(Guid commentParentID);
        /// <summary>
        /// hàm thực hiện thêm mới comment với comment đầu tiên
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        Task PostCommentRoot(Comments comment);
        /// <summary>
        /// hàm thực hiện lấy những commnet mới nhất
        /// </summary>
        /// <param name="numberComment"> số lượng comment muốn lấy</param>
        /// <returns></returns>
        Task<IEnumerable<CommentModel>> GetNewComment(int numberComment);

    }
}
