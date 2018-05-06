using System.Collections.Generic;

namespace OpenSim.WebServer.Model
{
    public interface IUserRepository
    {
        void Add(User user);
        User Get(long id);
        IEnumerable<User> GetAll();
        void Update(User user);
    }
}
