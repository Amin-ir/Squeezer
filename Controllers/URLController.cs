using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Squeezer.Services;
using Squeezer.ViewModels;

namespace Squeezer.Controllers
{
    [Route("/{controller}/{action}")]
    [Authorize(Policy = "AdminOnly")]
    public class URLController : Controller
    {
        URLManager URLManager;
        public URLController(URLManager urlManager)
        {
            URLManager = urlManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult List()
        {
            List<UrlUserPairViewModel> viewModel = new List<UrlUserPairViewModel>();
            URLManager.GetAll().ToList()
                .ForEach(url => 
                    viewModel.Add
                        (new UrlUserPairViewModel(url.User is null ? "Anonymous" : url.User.Name, url)));
            return View(viewModel);
        }
    }
}
