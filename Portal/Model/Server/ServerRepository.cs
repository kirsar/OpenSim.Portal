using System.Linq;
using OpenSim.Portal.Model;

namespace OpenSim.Portal.Model
{
    public class ServerRepository : IServerRepository
    {
        public ServerRepository(PortalDbContext context)
        {
            this.context = context;
        }

        public IQueryable<Server> GetAll() => context.Servers;

        public Server Get(long id)
        {
            throw new System.NotImplementedException();
        }

        public void Add(Server server)
        {
            throw new System.NotImplementedException();
        }

        public Server Remove(long id)
        {
            throw new System.NotImplementedException();
        }

        public void Update(Server server)
        {
            throw new System.NotImplementedException();
        }

        private readonly PortalDbContext context;
    }
}