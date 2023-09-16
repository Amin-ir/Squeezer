using Microsoft.AspNetCore.Mvc;
using Squeezer.Models;
using Squeezer.Services;

namespace Squeezer.Components
{
    public class AdminNavigationViewComponent : ViewComponent
    {
        IAuthenticator Authenticator;
        public AdminNavigationViewComponent(IAuthenticator authenticator)
        {
            Authenticator = authenticator;
        }       
        public async Task<IViewComponentResult> InvokeAsync()
        {            
            if (Authenticator.GetUserRole() == UserRole.Admin.ToString())
                return View(true);
            else 
                return View(false);
        }
    }
}
