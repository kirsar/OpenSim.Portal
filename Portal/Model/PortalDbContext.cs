using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace OpenSim.Portal.Model
{
    public class PortalDbContext : DbContext
    {
        // ReSharper disable once SuggestBaseTypeForParameter
        public PortalDbContext(DbContextOptions<PortalDbContext> options) : base(options)
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
            builder.Entity<SimulationReference>()
                .HasOne(bc => bc.Simulation)
                .WithMany(b => b.SimulationReferences)
                .HasForeignKey(bc => bc.SimulationId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<SimulationReference>()
                .HasOne(bc => bc.Reference)
                .WithMany(c => c.SimulationReferencesBackRef)
                .HasForeignKey(bc => bc.ReferenceId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<SimulationPresentation>()
                .HasKey(e => new { e.SimulationId, e.PresentationId });
        }
    }
}