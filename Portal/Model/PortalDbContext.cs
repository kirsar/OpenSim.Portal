using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace OpenSim.Portal.Model.User
{
    public class PortalDbContext : DbContext
    {
        public PortalDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Server.Server> Servers { get; set; }
        public DbSet<Simulation.Simulation> Simulations { get; set; }
        public DbSet<Presentation.Presentation> Presentations { get; set; }
    }
}