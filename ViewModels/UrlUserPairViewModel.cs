using Squeezer.Models;

namespace Squeezer.ViewModels
{
    public class UrlUserPairViewModel
    {
        public readonly string UserName;
        public readonly Url Url;
        public UrlUserPairViewModel(string userName, Url url)
        {
            UserName = userName;
            Url = url;
        }
    }
}
