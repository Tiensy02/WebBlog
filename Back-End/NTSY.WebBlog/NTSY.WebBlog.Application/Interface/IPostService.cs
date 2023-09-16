using NTSY.WebBlog.Application.Dto.Post;
using NTSY.WebBlog.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSY.WebBlog.Application
{
    public interface IPostService :IBaseService<PostInsertDto, PostUpdateDto, Posts>
    {
        /// <summary>
        /// Hàm lấy danh sách bài viết theo phân trang
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns> </returns>
        Task<(IEnumerable<PostModel>, int)> GetPostForUI(int page, int pageSize);
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
        Task<IEnumerable<PostModel>> GetPostByFilter(string filter);
        /// <summary>
        /// hàm thực hiện cập nhập điểm đánh giá và lượt đánh giá của bài viết
        /// </summary>
        /// <param name="ratingCore"></param>
        /// <param name="postID"></param>
        /// <returns></returns>

        Task UpdateRating(float ratingCore, Guid postID);
        /// <summary>
        /// hàm thực hiện lấy danh sách bài viết theo id cảu user
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        Task <(IEnumerable<PostModel>, int)> GetPostsOfUser(Guid userID, int page, int pageSize);
    }
}
