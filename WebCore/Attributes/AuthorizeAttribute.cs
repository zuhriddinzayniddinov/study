using Entity.Enum;
using Microsoft.AspNetCore.Mvc;
using WebCore.Filters;

namespace WebCore.Attributes;

public class AuthorizeAttribute : TypeFilterAttribute
{
    public AuthorizeAttribute(params UserPermissions[] permissions) : base(typeof(PermissionRequirementFilter))
    {
        this.Arguments = new object[]
        {
            permissions.Cast<int>().ToArray()
        };
    }
}