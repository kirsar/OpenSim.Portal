using System.Collections.Generic;
using System.Collections.Concurrent;

namespace OpenSim.WebServer.App.Controllers.Server
{
    public class ServerRepository : IServerRepository
    {
        private ConcurrentDictionary<int, Server> servers = new ConcurrentDictionary<int, Server>();
        private int currentId;

        private int GetId() => currentId++;

        public void Add(Server server)
        {
            var id = GetId();
            server.Id = id;
            servers[id] = server;
        }

        public Server Find(int id)
        {
            servers.TryGetValue(id, out Server server);
            return servers[id];
        }

        public IEnumerable<Server> GetAll() => servers.Values;

        public Server Remove(int id)
        {
            servers.TryRemove(id, out Server server);
            return server;
        }

        public void Update(Server server) => servers[server.Id] = server;
    }
}
