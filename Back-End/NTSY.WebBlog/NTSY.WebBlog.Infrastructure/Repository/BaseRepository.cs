using Dapper;
using NTSY.WebBlog.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace NTSY.WebBlog.Infrastructure
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
    {
        protected readonly IUnitOfWork _uow;
        // Tên bảng mặc định trùng với tên của Entity 
        public virtual string TableName { get; protected set; } = typeof(TEntity).Name;
        // Tên trường ID mặc định trùng với tên bảng + ID
        public virtual string ColumnID { get; protected set; } = typeof(TEntity).Name + "ID";
        // tên procedure thêm mới
        public virtual string ProcInsert { get; protected set; } = $"Proc_{typeof(TEntity).Name}_Insert";
        // tên procedure chỉnh sửa
        public virtual string ProcUpdate { get; protected set; } = $"Proc_{typeof(TEntity).Name}_Update";


        protected BaseRepository(IUnitOfWork uow) {
            this._uow = uow;
        }
        

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var sql = $"SELECT * FROM {TableName}; ";
            var result = await _uow.Connection.QueryAsync<TEntity>(sql);
            return result;
        }

        public async Task<TEntity> GetAsync(Guid id)
        {
            var sql = $"SELECT * FROM {TableName} WHERE {ColumnID} = @id;";
            var param = new DynamicParameters();
            param.Add("@id", id);
            var result = await _uow.Connection.QueryFirstOrDefaultAsync<TEntity>(sql, param);
            return result;
        }

        public async Task InsertAsync(TEntity entity)
        {
            var param = new DynamicParameters();
            var type = typeof(TEntity);
            var properties = type.GetProperties();
            foreach(var property in properties)
            {
                var propertyName = property.Name;
                var propertyValue = property.GetValue(entity);
                param.Add(propertyName, propertyValue);
            }
            await _uow.Connection.ExecuteAsync(ProcInsert, param, commandType: CommandType.StoredProcedure);

        }

        public async Task UpdateAsync(TEntity entity)
        {
            var param = new DynamicParameters();
            var type = typeof(TEntity);
            var properties = type.GetProperties();
            foreach (var property in properties)
            {
                var propertyName = property.Name;
                var propertyValue = property.GetValue(entity);
                param.Add(propertyName, propertyValue);
            }
            await _uow.Connection.ExecuteAsync(ProcUpdate, param, commandType: CommandType.StoredProcedure);

        }
        public async Task DeleteAsync(Guid id)
        {
            var sql = $"DELETE FROM {TableName} WHERE {ColumnID} = @id;";
            var param = new DynamicParameters();
            param.Add("@id", id);
            await _uow.Connection.ExecuteAsync(sql, param);
        } 

        public async Task DeleteMultiAsync(List<Guid> ids)
        {
            var sql = $"DELETE FROM {TableName} WHERE {ColumnID} IN @ids;";
            var param = new DynamicParameters();
            param.Add("@ids", ids);
            await _uow.Connection.ExecuteAsync(sql, param);
        }
    }
}
