using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace OpenSim.Portal.Model
{
    public class ServerRepository : IServerRepository
    {
        public ServerRepository(PortalDbContext context) => this.context = context;

        public IQueryable<Server> GetAll() => context.Servers
            .WithSimulations()
            .WithPresentations();

        public Server Get(int id) => context.Servers.Where(s => s.Id == id)
            .WithSimulations()
            .WithPresentations()
            .SingleOrDefault();

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

    public static class ServerRepositoryExtensions
    {
        public static IQueryable<Server> WithSimulations(this IQueryable<Server> servers) => servers
            .Include(e => e.ServerSimulations)
            .ThenInclude(e => e.Simulation);

        public static IQueryable<Server> WithPresentations(this IQueryable<Server> servers) => servers
            .Include(e => e.ServerPresentations)
            .ThenInclude(e => e.Presentation);
    }
}