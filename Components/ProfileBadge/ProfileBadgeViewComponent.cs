using Microsoft.AspNetCore.Mvc;
using Squeezer.Services;

namespace Squeezer.Components
{
    public class ProfileBadgeViewComponent : ViewComponent
    {
        IAuthenticator Authenticator;
        UserManager UserManager;
        public ProfileBadgeViewComponent(IAuthenticator authenticator, UserManager userManager)
        {
            Authenticator = authenticator;
            UserManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            if(Authenticator.IsAuthenticated())
            {
                int userId = Authenticator.GetUserId();
                var user = UserManager.Get(userId);
                ViewBag.HasSignedIn = true;
                return View(user);
            }
            return View(UserManager.GetEmptyUser());
        }
    }
}
