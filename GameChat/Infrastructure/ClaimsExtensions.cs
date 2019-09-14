using System.Security.Claims;

namespace GameChat.Web.Infrastructure
{
    public static class ClaimsExtensions
    {
        public static int GetUserId(this ClaimsPrincipal principal)
        {
            return int.Parse(principal.FindFirstValue(ClaimTypes.NameIdentifier));
        }
    }
}
