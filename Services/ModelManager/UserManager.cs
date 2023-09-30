using Squeezer.Infrastructure;
using Squeezer.Models;

namespace Squeezer.Services
{
    public class UserManager : IModelManager<User>
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
        public bool IsDuplicate(User user)
        {
            return db.Users.Any(_user => _user.Email == user.Email);
        }
        public User Get(int id)
        {
            return db.Users.First(user => user.Id == id);
        }
        public User Get(string email)
        {
            return db.Users.First(user => user.Email == email);
        }
        public IEnumerable<User> GetAll()
        {
            return db.Users;
        }
        public bool IsCredentialsValid(User user)
        {
            return db.Users.Any
                (_user => _user.Email == user.Email && Encryptor.EncryptToString(user.Password) == _user.Password);
        }
        public UserRole GetRole(int id)
        {
            return Get(id).UserRole;
        }
        public User GetEmptyUser()
        {
            return new User();
        }
    }
}
