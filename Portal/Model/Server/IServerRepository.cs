using System.Linq;

namespace OpenSim.Portal.Model
{
    public interface IServerRepository
    {
        IQueryable<Server> GetAll();
        Server Get(int id);
        void Add(Server server);
        Server Remove(int id);
        void Update(Server server);
    }
}
