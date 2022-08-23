using System.Security.Claims;

using Dietonator.Application.Common.Interfaces;

namespace Dietonator.WebUI.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid? UserId
    {
        get
        {
            //var claimValue = _httpContextAccessor
            //    .HttpContext?
            //    .User?
            //    .FindFirstValue(ClaimTypes.NameIdentifier);

            //if (claimValue == null)
            //{
            //    return null;
            //}

            //var isGuidParsed = Guid.TryParse(claimValue, out var id);

            //return isGuidParsed ? id : null;

            return Guid.Parse("7308E922-31B5-4EEB-B5B8-4F07918E9722");
        }
    }
}
