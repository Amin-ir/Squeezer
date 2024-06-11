using Squeezer.Models;
using Squeezer.Services.Builders;

namespace Squeezer.Services.Directors;

public class UserDirector : IUserDirector
{
    private readonly IUserBuilder _userBuilder;

    public UserDirector(IUserBuilder userBuilder)
    {
        _userBuilder = userBuilder;
    }

    public User CreateAdminUser()
    {
        var admin = _userBuilder
                   .SetName("INITIAL ADMIN")
                   .SetEmail("admin@squeezer.com")
                   .SetRole(UserRole.Admin)
                   .SetPassword("admin")
                   .Create();
        
        return admin;
    }
}
