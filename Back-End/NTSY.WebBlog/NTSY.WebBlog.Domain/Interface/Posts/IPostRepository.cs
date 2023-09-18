using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSY.WebBlog.Domain
{
    public interface IPostRepository : IBaseRepository<Posts>
    {
        /// <summary>
        /// Hàm lấy danh sách bài viết theo phân trang
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns> </returns>
        Task<(IEnumerable<PostModel>,int)> GetPostForUI(int page, int pageSize);
        /// <summary>
        /// lấy bài viết theo id của bài viết
        /// </summary>
        /// <param name="postID"></param>
        /// <returns></returns>
        Task<PostModel> GetPostByID(Guid postID);
        /// <summary>
        /// hàm thực hiện lấy danh sách bài viết theo kí tự tìm kiếm
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>

        Task<(IEnumerable<PostModel>,int)> GetPostByFilter(string filter, int page, int pageSize);
        /// <summary>
        /// hàm thực hiện cập nhập điểm cho bài viết
        /// </summary>
        /// <param name="ratingCore"></param>
        /// <param name="postID"></param>
        /// <returns></returns>
        Task UpdateRating (float ratingCore, Guid postID);

        /// <summary>
        /// hàm thực hiện lấy danh sách bài viết của 1 người dùng cụ thể 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        Task<(IEnumerable<PostModel>, int)> GetPostByUser(Guid userID, int page, int pageSize);



    }
}
