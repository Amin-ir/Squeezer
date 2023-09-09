using Microsoft.AspNetCore.Mvc;
using Squeezer.Infrastructure;
using Squeezer.Models;
using Squeezer.Services;

namespace Squeezer.Controllers
{
    public class UserController : Controller
    {
        UserManager UserManager;
        public UserController(UserManager userManager)
        {
            UserManager = userManager;
        }
        [Route("/Dashboard")]
        public IActionResult Index()
        {
            return View();
        }
        [Route("/User/SignUp")]
        [HttpPost]
        public IActionResult SignUp([FromForm]User user)
        {
            if (ModelState.IsValid)
            {
                UserManager.Create(user);
                return RedirectToAction("Index", "User");
            }
            return RedirectToAction("SignUp", "Home");
        }
    }
}
