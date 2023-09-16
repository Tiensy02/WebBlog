using Dapper;
using NTSY.WebBlog.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSY.WebBlog.Infrastructure
{
    public class NotificationRepository : BaseRepository<Notification>, INotificationRepository
    { 
        public NotificationRepository(IUnitOfWork uow) : base(uow)
        {
        } 

        public async Task<IEnumerable<Notification>> GetNotificationByUserID(Guid userID)
        {
            var param = new DynamicParameters(); 
            param.Add("UserID",userID.ToString());
            var result = await _uow.Connection.QueryAsync<Notification>("Proc_Notification_GetNotificationByUserID", param, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public async Task UpdateNotificationStatus(List<Guid> ids)
        {
            var statusValue = (int)NotificationStatus.IsRead;
            var sql = $"UPDATE {TableName} SET NotificationStatus = {statusValue} WHERE NotificationID IN @ids ";
            var param = new DynamicParameters();
            param.Add("@ids", ids);
            await _uow.Connection.ExecuteAsync(sql, param);
        }
    }
}
