using Microsoft.AspNetCore.Mvc;
using Squeezer.Models;

namespace Squeezer.Services
{
    public interface IAuthenticator
    {
        public bool IsAuthenticated();
        public Task<IActionResult> LogIn(User user);
        public Task<IActionResult> LogOut();
        public int GetUserId();
    }
}
