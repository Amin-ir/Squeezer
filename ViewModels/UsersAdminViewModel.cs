using Squeezer.Models;

namespace Squeezer.ViewModels
{
    public class UsersAdminViewModel
    {
        public readonly User User;
        public readonly int GeneratedUrlCount;
        public UsersAdminViewModel(User user, int generatedUrlCount)
        {
            User = user;
            GeneratedUrlCount = generatedUrlCount;
        }
    }
}
