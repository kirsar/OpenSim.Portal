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

        public IQueryable<Server> GetAll() => IncludeRelations(context.Servers);

        public Server Get(int id) => IncludeRelations(context.Servers).SingleOrDefault(s => s.Id == id);

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

        private static IQueryable<Server> IncludeRelations(IQueryable<Server> servers) => servers
            .Include(e => e.ServerSimulations)
            .ThenInclude(e => e.Simulation)
            .Include(e => e.ServerPresentations)
            .ThenInclude(e => e.Presentation);

        private readonly PortalDbContext context;
    }
}