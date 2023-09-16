using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Squeezer.Models;
using System.Security.Claims;

namespace Squeezer.Services
{
    public class ClaimBasedAuthenticator : IAuthenticator
    {
        IHttpContextAccessor HttpContextAccessor;
        public ClaimBasedAuthenticator(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
        }
        public bool IsAuthenticated() 
        {
            string? roleClaimValue = HttpContextAccessor.HttpContext?.User.FindFirst("Role")?.Value;
            return roleClaimValue == UserRole.Admin.ToString() || roleClaimValue == UserRole.TypicalUser.ToString();
        }
        public async Task<IActionResult> LogIn(User user)
        {
            var claims = new List<Claim> { new Claim("Role", user.UserRole.ToString()), new Claim("Id", user.Id.ToString()) };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContextAccessor.HttpContext.SignInAsync(principal);
            return null;
        }
        public async Task<IActionResult> LogOut()
        {
            await HttpContextAccessor.HttpContext.SignOutAsync();
            return null;
        }
        public int GetUserId()
        {
            var claimIdValue = HttpContextAccessor.HttpContext.User.FindFirst("Id")?.Value;
            return Convert.ToInt32(claimIdValue);
        }
    }
}
