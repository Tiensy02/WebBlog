﻿using Dapper;
using NTSY.WebBlog.Domain;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSY.WebBlog.Infrastructure
{
    public class FollowsRepository : BaseRepository<Follows>, IFollowsRepository
    {
        public override string ColumnID { get; protected set; } = "FollowID";
        public FollowsRepository(IUnitOfWork uow) : base(uow)
        {
        }
        /// <summary>
        /// hàm thực hiện lấy những người đang theo dõi user 
        /// </summary> 
        /// <param name="userFlollowID">id của người được theo dõi</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(IEnumerable<FollowModel>, int)> GetUserFlollowed(Guid userFlollowID, Guid userCurrentID, int page, int pageSize)
        {
            var param = new DynamicParameters();
            param.Add("FollowedID", userFlollowID);
            param.Add("page", page);
            param.Add("userCurrentID", userCurrentID);
            param.Add("pageSize", pageSize);
            param.Add("totalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
            var result = await _uow.Connection.QueryAsync<FollowModel>("Proc_Follows_GetUserFollowed", param, commandType: System.Data.CommandType.StoredProcedure);
            var totalRecord = param.Get<int>("totalRecord");
            return (result, totalRecord);
        }
        /// <summary>
        /// hàm thực hiện lấy những người user đang theo dõi
        /// </summary>
        /// <param name="userFollowingID"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<(IEnumerable<FollowModel>, int)> GetUserFollowing(Guid userFollowingID, Guid userCurrentID, int page, int pageSize)
        {
            var param = new DynamicParameters();
            param.Add("UserFollowID", userFollowingID);
            param.Add("page", page);
            param.Add("userCurrentID", userCurrentID);
            param.Add("pageSize", pageSize);
            param.Add("totalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
            var result = await _uow.Connection.QueryAsync<FollowModel>("Proc_Follow_GetUserFollowing", param, commandType: System.Data.CommandType.StoredProcedure);
            var totalRecord = param.Get<int>("totalRecord");
            return (result, totalRecord);
        }
        public async Task DeleteFollowAsync(Guid followingID, Guid followedID)
        {
            var param = new DynamicParameters();
            param.Add("followingID", followingID);
            param.Add("followedID", followedID);
            await _uow.Connection.QueryFirstOrDefaultAsync<Follows>("Proc_Follow_Delete", param, commandType: CommandType.StoredProcedure);

        }
    }

}
