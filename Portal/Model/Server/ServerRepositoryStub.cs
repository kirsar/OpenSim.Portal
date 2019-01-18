using System.Collections.Concurrent;
using System.Linq;

namespace OpenSim.Portal.Model
{
    public class ServerRepositoryStub : IServerRepository
    {
        private ConcurrentDictionary<long, Server> servers = new ConcurrentDictionary<long, Server>();
        private int currentId;

        private int GetId() => currentId++;

        public void Add(Server server)
        {
            var id = GetId();
            server.Id = id;
            servers[id] = server;
        }

        public Server Get(int id)
        {
            servers.TryGetValue(id, out var server);
            return server;
        }

        public IQueryable<Server> GetAll() => servers.Values.AsQueryable();

        public Server Remove(int id)
        {
            servers.TryRemove(id, out var server);
            return server;
        }

        public void Update(Server server) => servers[server.Id] = server;
    }
}
