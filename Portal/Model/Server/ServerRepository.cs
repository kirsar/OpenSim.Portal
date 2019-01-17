using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace OpenSim.Portal.Model
{
    public class ServerRepository : IServerRepository
    {
        public ServerRepository(PortalDbContext context)
        {
            this.context = context;
        }

        public IQueryable<Server> GetAll() => context.Servers
            .Include(e => e.Simulations)
            .Include(e => e.Presentations);

        public Server Get(int id) => context.Servers.Find(id);

        public void Add(Server server)
        {
            context.Servers.Add(server);
            context.SaveChanges();
        }

        public Server Remove(int id)
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