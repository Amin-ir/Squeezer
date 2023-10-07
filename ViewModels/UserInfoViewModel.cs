using Squeezer.Models;

namespace Squeezer.ViewModels
{
    public class UserInfoViewModel
    {
        public readonly User User;
        public readonly List<Url> Urls;
        public UserInfoViewModel(User user, List<Url> urls)
        {
            User = user;
            Urls = urls;
        }
    }
}
