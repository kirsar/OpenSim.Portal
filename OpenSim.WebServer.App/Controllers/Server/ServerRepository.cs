using System.Collections.Generic;
using System.Collections.Concurrent;

namespace OpenSim.WebServer.App.Controllers.Server
{
    public class ServerRepository : IServerRepository
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

        public Server Get(long id)
        {
            servers.TryGetValue(id, out var server);
            return server;
        }

        public IEnumerable<Server> GetAll() => servers.Values;

        public Server Remove(long id)
        {
            servers.TryRemove(id, out var server);
            return server;
        }

        public void Update(Server server) => servers[server.Id] = server;
    }
}
