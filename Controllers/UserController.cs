using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Squeezer.Infrastructure;
using Squeezer.Models;
using Squeezer.Services;
using System.Security.Claims;

namespace Squeezer.Controllers
{
    [Route("/{controller}/{action}")]
    public class UserController : Controller
    {
        UserManager UserManager;
        IAuthenticator Authenticator;
        public UserController(UserManager userManager, IAuthenticator authenticator)
        {
            UserManager = userManager;
            Authenticator = authenticator;
        }

        [Authorize(Policy = "UserAccessible")]
        [Route("/Dashboard")]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SignUp()
        {
            var u = User;
            if (Authenticator.IsAuthenticated())
                return RedirectToAction("Index");
            return View();
        }

        [HttpPost]
        public IActionResult SignUp([FromForm]User user)
        {
            if (ModelState.IsValid)
            {
                user = UserManager.Create(user);
                Authenticator.LogIn(user.Id);
                return RedirectToAction("Index");
            }
            return RedirectToAction();
        }

        public IActionResult SignIn()
        {
            if (Authenticator.IsAuthenticated())
                return RedirectToAction("Index");
            return View();
        }

        [HttpPost]
        public IActionResult SignIn([FromForm]User user) 
        {
            if(UserManager.IsCredentialsValid(user))
            {
                int userId = UserManager.GetId(user.Email);
                Authenticator.LogIn(userId);
                return RedirectToAction("Index");
            }
            return RedirectToAction();
        }
        public IActionResult SignOut()
        {
            Authenticator.LogOut();
            return RedirectToAction("SignIn");
        }
    }
}
