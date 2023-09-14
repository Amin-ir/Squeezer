using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Squeezer.Models;
using Squeezer.Services;
using Squeezer.Utils;
using Squeezer.ViewModels;

namespace Squeezer.Controllers
{
    [Route("/{controller}/{action}")]
    public class UserController : Controller
    {
        UserManager UserManager;
        URLManager UrlManager;
        IAuthenticator Authenticator;
        public UserController(UserManager userManager, URLManager urlManager, IAuthenticator authenticator)
        {
            UserManager = userManager;
            Authenticator = authenticator;
            UrlManager = urlManager;
        }

        [Authorize(Policy = "UserAccessible")]
        [Route("/Dashboard")]
        public IActionResult Index()
        {
            int userId = Authenticator.GetUserId();
            UrlListViewModel model = new UrlListViewModel
            {
                Urls = UrlManager.Get(userId).ToList()
            };

            string domain = HttpContext.GetHostDomain();
            model.Urls.ForEach(url => url.ShortenedUrl = $"{domain}/{url.ShortenedUrl}");

            return View(model);
        }

        public IActionResult SignUp()
        {
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
            ViewBag.Errors = ModelState.GetModelErrorTexts();
            return View();
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
            ViewBag.Errors = "Password or Email doesn't match";
            return View();
        }
        public IActionResult SignOut()
        {
            Authenticator.LogOut();
            return RedirectToAction("SignIn");
        }
    }
}
