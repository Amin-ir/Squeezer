using Microsoft.AspNetCore.Mvc;
using Squeezer.Models;
using Squeezer.Services;
using Squeezer.Utils;

namespace Squeezer.Controllers
{
    public class HomeController : Controller
    {
        URLManager URLManager;
        public HomeController(URLManager urlManager)
        {
            URLManager = urlManager;
        }

        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }
        
        [Route("/Signup")]
        public IActionResult SignUp()
        {
            return View();
        }
        
        [Route("/SignIn")]
        public IActionResult SignIn()
        {
            return View();
        }
        
        [HttpPost]
        [Route("/Squeeze")]
        public IActionResult Squeeze([FromForm]Url url)
        {
            if(ModelState.IsValid)
            {
                url = URLManager.Create(url);

                string domain = HttpContext.Request.Host.Value;

                url.ShortenedUrl = $"{domain}/{url.ShortenedUrl}";

                return View("Result", url);
            }
            ViewBag.Errors = ModelState.GetModelErrorTexts();
            return View("Index");
        }

        [Route("/{shortUrl:shortUrlConstraint}")]
        public IActionResult UnSqueeze(string shortUrl)
        {
            try
            {
                string originalUrl = URLManager.GetOriginalUrl(shortUrl);
                return Redirect(originalUrl);
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
