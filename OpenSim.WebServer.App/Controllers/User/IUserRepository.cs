using System.Collections.Generic;

namespace OpenSim.WebServer.App.Controllers.User
{
    public interface IUserRepository
    {
        void Add(User user);
        User Get(long id);
        UserDetails GetDetails(long id);
        IEnumerable<User> GetAll();
        void Update(User user);
    }
}
