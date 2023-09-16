using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NTSY.WebBlog.Application;
using NTSY.WebBlog.Domain;
using NTSY.WebBlog.Infrastructure;
using System.Security.Cryptography.X509Certificates;

namespace NTSY.WebBlog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : BaseController<NotificationInsertDto, Notification, Notification>
    {
        private readonly INotificationService _notificationService;
        public NotificationController(INotificationService baseService) : base(baseService)
        {
            _notificationService = baseService;
        }
        [HttpPut("Many")]
        public async Task<IActionResult> UpdateNotificationStatus(List<Guid> ids)
        {
            await _notificationService.UpdateNotificationStatus(ids);
            return StatusCode(StatusCodes.Status200OK);
        }
        [HttpGet("{userID}/notification")]
        public async Task<IEnumerable<Notification>> GetNotificationByUserID(Guid userID)
        {
            var result = await _notificationService.GetNotificationByUserID(userID);
            return result;
        }
    }
}
