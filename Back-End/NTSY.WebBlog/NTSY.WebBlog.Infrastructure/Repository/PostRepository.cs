using Dapper;
using NTSY.WebBlog.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSY.WebBlog.Infrastructure
{
    public class PostRepository : BaseRepository<Posts>, IPostRepository
    {
        public PostRepository(IUnitOfWork uow) : base(uow)
        {
        }

        public async Task<(IEnumerable<PostModel>, int)> GetPostByFilter(string filter, int page, int pageSize)
        {
            var param = new DynamicParameters();
            param.Add("@textSearch", filter);
            param.Add("@page", page);
            param.Add("@pageSize", pageSize);
            param.Add("totalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
            var result = await _uow.Connection.QueryAsync<PostModel>("Proc_Posts_Filter", param, commandType: CommandType.StoredProcedure);
            var totalRecord = param.Get<int>("totalRecord");
            return (result, totalRecord);

        }

        /// <summary>
        /// Hàm lấy 1 bài viết theo ID của bài viết
        /// </summary>
        /// <param name="postID"> id của bài viết</param>
        /// <returns></returns>
        public async Task<PostModel> GetPostByID(Guid postID)
        {   
            var param = new DynamicParameters();
            param.Add("id", postID);
            var result = await _uow.Connection.QueryFirstOrDefaultAsync<PostModel>("Proc_Posts_GetPostByID", param, commandType:CommandType.StoredProcedure);
            return result;
        }
        /// <summary>
        /// hàm thực hiện lấy danh sách bài viết theo id của user 
        /// </summary>
        /// <param name="userID">id của người dùng</param>
        /// <returns></returns>
        public async Task<(IEnumerable<PostModel>, int)> GetPostByUser(Guid userID, int page, int pageSize)
        {
            var param = new DynamicParameters();
            param.Add("@id", userID);
            param.Add("@page", page);
            param.Add("@pageSize", pageSize);
            param.Add("totalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
            var result = await _uow.Connection.QueryAsync<PostModel>("Proc_Posts_GetPostOfUser",param,commandType:CommandType.StoredProcedure);
            var totalRecord = param.Get<int>("totalRecord");
            return (result,totalRecord);
        }

        /// <summary>
        /// Hàm lấy danh sách bài viết có phân trang
        /// </summary>
        /// <param name="page">Trang hiện tại</param>
        /// <param name="pageSize">Tổng số bant ghi trên 1 trang</param>
        /// <returns></returns>
        public async Task<(IEnumerable<PostModel>,int)> GetPostForUI(int page, int pageSize)
        {
            var param = new DynamicParameters();
            param.Add("page", page);
            param.Add("pageSize", pageSize);
            param.Add("totalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
            var result = await _uow.Connection.QueryAsync<PostModel>("Proc_Posts_GetPostsWithPage", param, commandType: CommandType.StoredProcedure);
            var totalRecord = param.Get<int>("totalRecord");
            return (result, totalRecord);
        }

        public async Task UpdateRating(float ratingCore, Guid postID)
        {
            var param = new DynamicParameters();
            param.Add("@ratingCore", ratingCore);
            param.Add("@postID", postID);
            await _uow.Connection.ExecuteAsync("Proc_Post_UpdateRating", param, commandType: CommandType.StoredProcedure);
        }
        
    }
}
