using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace OpenSim.Portal.Model
{
    public class PortalDbContext : DbContext
    {
        public PortalDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Server> Servers { get; set; }
        public DbSet<Simulation> Simulations { get; set; }
        public DbSet<Presentation> Presentations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ServerSimulation>()
                .HasKey(e => new { e.ServerId, e.SimulationId });
            builder.Entity<ServerPresentation>()
                .HasKey(e => new { e.ServerId, e.PresentationId });

            builder.Entity<SimulationReference>()
                .HasKey(e => new { e.SimulationId, e.ReferenceId });
            builder.Entity<SimulationPresentation>()
                .HasKey(e => new { e.SimulationId, e.PresentationId });
        }
    }
}