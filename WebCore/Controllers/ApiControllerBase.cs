using System.Security.Claims;
using AuthenticationBroker.TokenHandler;
using Microsoft.AspNetCore.Mvc;

namespace WebCore.Controllers;

public abstract class ApiControllerBase : ControllerBase
{
    protected int UserId => int.TryParse(User.FindFirstValue(CustomClaimNames.UserId), out var userId) ? userId : 0;
}