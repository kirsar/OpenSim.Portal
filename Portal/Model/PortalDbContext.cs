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
            builder.Entity<JoinEntity<Server, Simulation>>()
                .HasKey(e => new { e.FirstId, e.SecondId });
            builder.Entity<JoinEntity<Server, Presentation>>()
                .HasKey(e => new { e.FirstId, e.SecondId });

            builder.Entity<SimulationReference>()
                .HasKey(e => new { e.SimulationId, e.ReferenceId });
            builder.Entity<JoinEntity<Simulation, Presentation>>()
                .HasKey(e => new { e.FirstId, e.SecondId });

            base.OnModelCreating(builder);
        }
    }
}