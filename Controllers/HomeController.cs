using Microsoft.AspNetCore.Mvc;
using Squeezer.Services.URLShortener;
using System.ComponentModel.DataAnnotations;

namespace Squeezer.Controllers
{
    public class HomeController : Controller
    {
        IURLShortener UrlShortener;
        public HomeController(IURLShortener urlShortener)
        {
            UrlShortener = urlShortener;
        }

        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }
        
        [Route("/Squeeze")]
        public IActionResult Squeeze([FromBody][Url]string url)
        {
            if(ModelState.IsValid)
            {
                string shortenedURL = UrlShortener.Shorten(url);
                return Content(shortenedURL);
            }
            return RedirectToAction("Index");
        }
    }
}
