using System.Collections.Generic;
using System.Linq;
using Financial.Chat.Domain.Shared.Entity;
using Financial.Chat.Domain.Shared.Handler;
using Financial.Chat.Domain.Shared.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Financial.Chat.Web.API.Controllers
{
    [ApiController]
    public class ApiController : Controller
    {
        protected readonly DomainNotificationHandler _notifications;
        protected readonly IMediatorHandler _mediator;

        protected ApiController(INotificationHandler<DomainNotification> notifications,
                                IMediatorHandler mediator)
        {
            _notifications = (DomainNotificationHandler)notifications;
            _mediator = mediator;
        }

        protected IEnumerable<DomainNotification> Notifications => _notifications.GetNotifications();

        protected bool IsValidOperation()
        {
            return (!_notifications.HasNotifications());
        }

        protected new IActionResult Response(object result = null)
        {
            if (IsValidOperation())
            {
                return Ok(new ApiOkReturn
                {
                    success = true,
                    data = result
                });
            }

            return BadRequest(new ApiBadReturn
            {
                success = false,
                errors = _notifications.GetNotifications().Select(n => n.Value)
            });
        }

        protected void NotifyModelStateErrors()
        {
            var erros = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var erro in erros)
            {
                var erroMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotifyError(string.Empty, erroMsg);
            }
        }

        protected void NotifyError(string code, string message)
        {
            _mediator.RaiseEvent(new DomainNotification(code, message));
        }
    }
}
