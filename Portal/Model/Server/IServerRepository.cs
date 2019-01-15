using System.Collections.Generic;

namespace OpenSim.Portal.Model.Server
{
    public interface IServerRepository
    {
        void Add(Server server);
        IEnumerable<Server> GetAll();
        Server Get(long id);
        Server Remove(long id);
        void Update(Server server);
    }
}
