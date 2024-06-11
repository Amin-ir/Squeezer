using Squeezer.Models;

namespace Squeezer.Services.Builders;

public interface IUserBuilder
{
    public IUserBuilder SetName(string name);
    public IUserBuilder SetEmail(string email);
    public IUserBuilder SetRole(UserRole role);
    public IUserBuilder SetPassword(string password);
    public User Create();
}
