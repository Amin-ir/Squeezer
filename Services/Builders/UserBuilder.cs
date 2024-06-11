using Squeezer.Models;

namespace Squeezer.Services.Builders;

public class UserBuilder : IUserBuilder
{
    private User _user = new User();

    public readonly IEncryptor _encryptor;

    public UserBuilder(IEncryptor encryptor)
    {
        _encryptor = encryptor;
    }

    public User Create()
    {
        return _user;
    }

    public IUserBuilder SetEmail(string email)
    {
        _user.Email = email;
        return this;
    }

    public IUserBuilder SetName(string name)
    {
        _user.Name = name;
        return this;
    }

    public IUserBuilder SetPassword(string password)
    {
        _user.Password = _encryptor.EncryptToString(password);
        return this;
    }

    public IUserBuilder SetRole(UserRole role)
    {
        _user.UserRole = role;
        return this;
    }
}
