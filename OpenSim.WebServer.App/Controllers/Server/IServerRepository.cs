using System.Collections.Generic;

namespace OpenSim.WebServer.App.Controllers.Server
{
    public interface IServerRepository
    {
        void Add(Server server);
        IEnumerable<Server> GetAll();
        Server Find(int id);
        Server Remove(int id);
        void Update(Server server);
    }
}
