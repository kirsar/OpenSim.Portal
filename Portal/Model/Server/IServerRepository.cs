using System.Linq;

namespace OpenSim.Portal.Model.Server
{
    public interface IServerRepository
    {
        IQueryable<Server> GetAll();
        Server Get(long id);
        void Add(Server server);
        Server Remove(long id);
        void Update(Server server);
    }
}
