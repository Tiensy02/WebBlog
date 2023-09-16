using AutoMapper;
using NTSY.WebBlog.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTSY.WebBlog.Application
{
    public class BaseService<TEntityInsertDto, TEntityUpdateDto, TEntity> : IBaseService<TEntityInsertDto, TEntityUpdateDto, TEntity>
    {
        protected readonly IBaseRepository<TEntity> _baseRepository;
        protected readonly IMapper _mapper;
        protected BaseService(IBaseRepository<TEntity> baseRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }
        /// <summary>
        /// Hàm lấy tất cả bản ghi
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var result = await _baseRepository.GetAllAsync();
            return result;
        }
        /// <summary>
        /// hàm lấy 1 bản ghi theo id
        /// </summary>
        /// <param name="id">id của bản ghi</param>
        /// <returns></returns>
        public async Task<TEntity> GetAsync(Guid id)
        {
            var result = await _baseRepository.GetAsync(id);
            return result;
        }
        /// <summary>
        /// hàm thêm mới 1 bản ghi
        /// </summary>
        /// <param name="entityDto"></param>
        /// <returns></returns>
        public async Task InsertAsync(TEntityInsertDto entityDto)
        {
            var entity = _mapper.Map<TEntity>(entityDto);
            await _baseRepository.InsertAsync(entity);
        }
        /// <summary>
        /// hàm sửa 1 bản ghi
        /// </summary>
        /// <param name="entityDto"></param>
        /// <returns></returns>
        public async Task UpdateAsync(TEntityUpdateDto entityDto)
        {
            var entity = _mapper.Map<TEntity>(entityDto);
            await _baseRepository.UpdateAsync(entity);
        }
        public async Task DeleteAsync(Guid id)
        {
            await _baseRepository.DeleteAsync(id);
        }

        public async Task DeleteMultiAsync(List<Guid> ids)
        {
            await _baseRepository.DeleteMultiAsync(ids);
        }

    }
}
