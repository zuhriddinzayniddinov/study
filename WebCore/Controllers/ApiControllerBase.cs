using System.Security.Claims;
using AuthenticationBroker.TokenHandler;
using Microsoft.AspNetCore.Mvc;

namespace WebCore.Controllers;

public abstract class ApiControllerBase : ControllerBase
{
    public virtual long UserId
    {
        get
        {
            string? rawUserId = this.User.FindFirstValue(CustomClaimNames.UserId);
            if (long.TryParse(rawUserId, out long userId))
                return userId;
            else
                return default(long);
        }
    }
}