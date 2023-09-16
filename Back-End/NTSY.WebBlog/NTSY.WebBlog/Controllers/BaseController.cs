using Microsoft.AspNetCore.Mvc;
using NTSY.WebBlog.Application;

namespace NTSY.WebBlog
{
    public class BaseController<TEntityInsertDto, TEntityUpdateDto, TEntity> : Controller
    {
        public readonly IBaseService<TEntityInsertDto, TEntityUpdateDto, TEntity> _baseService;
        public BaseController(IBaseService<TEntityInsertDto, TEntityUpdateDto, TEntity> baseService)
        {
            _baseService = baseService;
        }
        [HttpGet]
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var resutl = await _baseService.GetAllAsync();
            return resutl;
        }
        [HttpGet("{id}")]
        public async Task<TEntity> GetAsync(Guid id)
        {
            var result = await _baseService.GetAsync(id);
            return result;
        }
        [HttpPost]
        public async Task<IActionResult> InsertAsync([FromBody] TEntityInsertDto entityInsertDto)
        {
            await _baseService.InsertAsync(entityInsertDto);
            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] TEntityUpdateDto entityUpdateDto)
        {
            await _baseService.UpdateAsync(entityUpdateDto);
            return StatusCode(StatusCodes.Status200OK);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteMultiAsync([FromBody] List<Guid> ids)
        {
            await _baseService.DeleteMultiAsync(ids);
            return StatusCode(StatusCodes.Status200OK);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await _baseService.DeleteAsync(id);
            return StatusCode(StatusCodes.Status200OK);
        }

    }
}
