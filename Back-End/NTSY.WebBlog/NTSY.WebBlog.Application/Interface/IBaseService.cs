using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSY.WebBlog.Application
{
    public interface IBaseService<TEntityInsertDto,TEntityUpdateDto, TEntity>
    {
        /// <summary>
        /// Hàm lấy toàn bộ bản ghi với các trường theo 1 đối tượng cụ thể  
        /// </summary>
        /// <param name="entity"> đối tượng cần lấy</param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetAllAsync();

        /// <summary>
        /// hàm lấy 1 bản ghi với các trường theo 1 đối tượng cụ thể
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TEntity> GetAsync(Guid id);
        /// <summary>
        /// Hàm thêm mới 1 bản ghi
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task InsertAsync(TEntityInsertDto entity);
        /// <summary>
        /// hàm sửa 1 bản ghi
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task UpdateAsync(TEntityUpdateDto entity);
        /// <summary>
        /// hàm xóa 1 bản ghi
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(Guid id);
        /// <summary>
        /// hàm 
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task DeleteMultiAsync(List<Guid> ids);

    }
}
