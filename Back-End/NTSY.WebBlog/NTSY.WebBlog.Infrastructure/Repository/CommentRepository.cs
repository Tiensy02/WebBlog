using Dapper;
using NTSY.WebBlog.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;


namespace NTSY.WebBlog.Infrastructure
{
    public class CommentRepository : BaseRepository<Comments>, ICommentRepository
    {
        public override string ColumnID {  get; protected set; } = "CommentID";
        public CommentRepository(IUnitOfWork uow) : base(uow)
        {
        }
        /// <summary>
        /// hàm lấy danh sách comment con của 1 comment cha dựa theo commentParentID
        /// </summary>
        /// <param name="commentParentID"> id của comment cha</param>
        /// <returns></returns>
        public async Task<IEnumerable<CommentModel>> GetCommentByCommentParentID(Guid commentParentID)
        {
            var param = new DynamicParameters();
            param.Add("@commentParentID", commentParentID);
            var result =  await _uow.Connection.QueryAsync<CommentModel>("Proc_Comments_GetCommentChildren", param, commandType:System.Data.CommandType.StoredProcedure);
            return result;
        }

        public async Task<IEnumerable<CommentModel>> GetCommentByPostIDForUI(Guid postID)
        {
            var param = new DynamicParameters();
            param.Add("PostID", postID);
            var result = await _uow.Connection.QueryAsync<CommentModel>("Proc_Comments_GetCommentWithPage", param, commandType: CommandType.StoredProcedure);
            return result;

        }

        public async Task PostCommentRoot(Comments comment)
        {
            var param = new DynamicParameters();
            var type = typeof(Comments);
            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                var propertyName = property.Name;
                var propertyValue = property.GetValue(comment);
                param.Add(propertyName, propertyValue);
            }
            await _uow.Connection.ExecuteAsync("Proc_Comment_InsertRoot", param, commandType: CommandType.StoredProcedure);

        }

        public async Task<IEnumerable<CommentModel>> GetNewComment(int numberComment)
        {
            var param = new DynamicParameters();
            param.Add("@numberComment", numberComment);
            var result = await _uow.Connection.QueryAsync<CommentModel>("Proc_Comments_GetNewComments", param,commandType:CommandType.StoredProcedure);
            return result;
        }
    }
}
