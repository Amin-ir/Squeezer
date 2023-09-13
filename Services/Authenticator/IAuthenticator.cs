using Microsoft.AspNetCore.Mvc;

namespace Squeezer.Services
{
    public interface IAuthenticator
    {
        public bool IsAuthenticated();
        public Task<IActionResult> LogIn(int id);
        public Task<IActionResult> LogOut();
        public int GetUserId();
    }
}
