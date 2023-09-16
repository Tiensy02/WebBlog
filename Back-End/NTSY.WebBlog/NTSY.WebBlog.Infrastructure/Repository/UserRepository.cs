using Dapper;
using NTSY.WebBlog.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace NTSY.WebBlog.Infrastructure
{
    public class UserRepository : BaseRepository<Users>, IUserRepository
    {
        public override string ColumnID { get; protected set; } = "UserID";
        public UserRepository(IUnitOfWork uow) : base(uow)
        {
        }

        public Task<Users> GetUserbyEmail(string email)
        {
            var param = new DynamicParameters(email);
            param.Add("userEmail", email);
            var result = _uow.Connection.QueryFirstOrDefaultAsync<Users>("Proc_User_GetUserByEmail", param, commandType: System.Data.CommandType.StoredProcedure);
            return result;
        }

        public async Task<Users> GetUserByNameAndPassword(Users user)
        {
            var param = new DynamicParameters();
            var type = typeof(Users);
            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                var propertyName = property.Name;
                var propertyValue = property.GetValue(user);
                param.Add(propertyName, propertyValue);
            }
         var result = await _uow.Connection.QueryFirstOrDefaultAsync<Users>("Proc_Users_Login", param, commandType: CommandType.StoredProcedure);
            return result;
        }
    }
}
