using System.Collections.Concurrent;
using System.Collections.Generic;

namespace OpenSim.WebServer.App.Controllers.User
{
    public class UserRepository : IUserRepository
    {
        private ConcurrentDictionary<long, User> users = new ConcurrentDictionary<long, User>();
        private long currentId;

        public void Add(User user)
        {
            var id = GetId();
            user.Id = id;
            users[id] = user;
        }

        public User Get(long id)
        {
            users.TryGetValue(id, out var user);
            return user;
        }

        public UserDetails GetDetails(long id) => Get(id)?.ToUserDetails();

        public IEnumerable<User> GetAll() => users.Values;

        public void Update(User user) => users[user.Id] = user;

        private long GetId() => currentId++;
    }
}
