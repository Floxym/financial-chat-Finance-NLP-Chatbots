
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Financial.Chat.Domain.Core.CommandHandlers;
using MediatR;
using Financial.Chat.Domain.Shared.Notifications;
using Financial.Chat.Domain.Shared.Handler;
using Microsoft.AspNetCore.Authorization;
using Financial.Chat.Domain.Shared.Entity;
using Microsoft.AspNetCore.Http;

namespace Financial.Chat.Web.API.Controllers
{
    [ApiController]
    [Route("api/login")]
    [AllowAnonymous]
    public class LoginController : ApiController
    {
        public LoginController(INotificationHandler<DomainNotification> notifications, IMediatorHandler mediator) : base(notifications, mediator)
        {
            
        }

        /// <summary>
        /// User's Login
        /// </summary>
        /// <remarks>
        /// POST /login
        /// {
        ///     "email": "test@test.com",
        ///     "password": "password"
        /// }
        /// </remarks>
        /// <param name="command"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK,Type = typeof(ApiOkReturn))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync(AuthenticateUserCommand command)
        {
            var token = await _mediator.SendCommandResult(command);
            bool success = token != null;

            if (success)
            {
                return Response(token);
            }

            return Unauthorized();
        }
    }
}