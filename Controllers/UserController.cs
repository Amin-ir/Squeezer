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
            var Urls = GetUrlsViewModel();
            return View(Urls);
        }

        UrlListViewModel GetUrlsViewModel()
        {
            int userId = Authenticator.GetUserId();
            UrlListViewModel model = new UrlListViewModel
            {
                Urls = UrlManager.GetByUser(userId).ToList()
            };

            string domain = HttpContext.GetHostDomain();
            model.Urls.ForEach(url => url.ShortenedUrl = $"{domain}/{url.ShortenedUrl}");

            return model;
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
            if (UserManager.IsDuplicate(user))
                ViewBag.Errors = new List<string> { "A user with the same Email already exists." };
            else if (!ModelState.IsValid)
                ViewBag.Errors = ModelState.GetModelErrorTexts();
            else
            {
                user = UserManager.Create(user);
                Authenticator.LogIn(user);
                return RedirectToAction("Index");
            }
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
                user = UserManager.Get(user.Email);
                Authenticator.LogIn(user);
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
        [Authorize(Policy = "AdminOnly")]
        public IActionResult List()
        {
            var usersList = GetUsersAdminViewModel();
            return View(usersList);
        }
        IEnumerable<UsersAdminViewModel> GetUsersAdminViewModel()
        {
            List<UsersAdminViewModel> users = new List<UsersAdminViewModel>();
            foreach (var user in UserManager.GetAll().ToList())
            {
                var userViewModel = new UsersAdminViewModel(user, UrlManager.GetUrlCountByUser(user.Id));
                users.Add(userViewModel);
            }
            return users;
        }
        [Route("/user/{id}")]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult Info(int id)
        {
            var viewModel = new UserInfoViewModel
                (UserManager.Get(id), UrlManager.GetByUser(id).ToList());
            return View(viewModel);
        }
    }
}
