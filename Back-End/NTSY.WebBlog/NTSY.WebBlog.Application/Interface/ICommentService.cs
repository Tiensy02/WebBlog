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
        /// hàm lấy danh sách comment trong 1 bài viết 
        /// </summary>
        /// <param name="postID">id của bài viết</param>
        /// <returns></returns>
        Task<IEnumerable<CommentDto>> GetCommentByPostIDForUI(Guid postID);
        /// <summary>
        /// lấy danh sách comment trả lời của 1 comment
        /// </summary>
        /// <param name="commentParentID"> id của comment cha </param>
        /// <returns></returns>
        Task<IEnumerable<CommentDto>> GetCommentByCommentParentID(Guid commentParentID);
        /// <summary>
        /// hàm thực hiện thêm mới 1 comment gốc ( comment ko có comment cha )  cho bài viết
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        Task PostCommentRoot(CommentInsertForPostDto comment);
        /// <summary>
        /// hàm thực hiện lấy những comment mới nhất
        /// </summary>
        /// <param name="numberComment"> số comment muốn lấy</param>
        /// <returns></returns>
        Task<IEnumerable<CommentDto>> GetNewComment(int numberComment);
    }
}
