using Squeezer.Infrastructure;
using Squeezer.Models;

namespace Squeezer.Services
{
    public class UserManager
    {
        SqueezerDbContext db;
        IEncryptor Encryptor;
        public UserManager(SqueezerDbContext db, IEncryptor encryptor)
        {
            this.db = db;
            Encryptor = encryptor;
        }
        public User Create(User user)
        {
            user.Password = Encryptor.EncryptToString(user.Password);
            
            db.Add(user);
            db.SaveChanges();
            
            return user;
        }
        public bool IsUserEmailDuplicate(string email)
        {
            return db.Users.Any(user => user.Email == email);
        }
    }
}
