using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Neo.Extensions.Core.Linq;
using Neo.Extensions.Notifications;

namespace Neo.Extensions.Api
{
    [Produces("application/json")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        public readonly ILogger Logger;
        public readonly IMediator Mediator;
        private readonly INotificationContext _notificationContext;

        protected BaseController(ILogger logger, IMediator mediator, INotificationContext notificationContext)
        {
            Logger = logger;
            Mediator = mediator;
            _notificationContext = notificationContext;
        }

        protected async Task<IActionResult> CreateResponse<T>(Func<Task<T>> function)
        {
            try
            {
                var data = await function();

                if (_notificationContext.HasNotifications)
                    return UnprocessableEntity(new Response(notifications: _notificationContext.Notifications.Select(x => new { x.Key, x.Message })));

                return Ok(new Response(data, true));
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"An exception was thrown while executing the method {function.Method.Name}");

                return StatusCode(500, new Response(notifications: ex.FromHierarchy(e => e.InnerException)
                    .Select(e => e.Message)));
            }
        }
    }
}
