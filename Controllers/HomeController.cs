using Microsoft.AspNetCore.Mvc;
using Squeezer.Models;
using Squeezer.Services;

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
            //Remember to add a partial view for this idea : what partial view? the <div> element inputs are in! take it out of layout
            //Because i want user dashboard to have the background of the main layout. only a .5-opacity div as a container for user's data
            ViewBag.Errors = ModelState.GetModelErrorTexts();
            return View("Index");
        }
    }
    
}
