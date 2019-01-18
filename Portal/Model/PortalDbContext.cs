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
            builder.Entity<ServerSimulation>()
                .HasOne(e => e.Server)
                .WithMany(e => e.ServerSimulations)
                .HasForeignKey(e => e.ServerId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<ServerSimulation>()
                .HasOne(e => e.Simulation)
                .WithMany(e => e.ServerSimulationsBackRef)
                .HasForeignKey(e => e.SimulationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ServerPresentation>()
                .HasKey(e => new { e.ServerId, e.PresentationId });
            builder.Entity<ServerPresentation>()
                .HasOne(e => e.Server)
                .WithMany(e => e.ServerPresentations)
                .HasForeignKey(e => e.ServerId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<ServerPresentation>()
                .HasOne(e => e.Presentation)
                .WithMany(e => e.ServerPresentationBackRef)
                .HasForeignKey(e => e.PresentationId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.Entity<SimulationReference>()
                .HasKey(e => new { e.SimulationId, e.ReferenceId });
            builder.Entity<SimulationReference>()
                .HasOne(e => e.Simulation)
                .WithMany(e => e.SimulationReferences)
                .HasForeignKey(e => e.SimulationId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<SimulationReference>()
                .HasOne(e => e.Reference)
                .WithMany(e => e.SimulationReferencesBackRef)
                .HasForeignKey(e => e.ReferenceId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<SimulationPresentation>()
                .HasKey(e => new { e.SimulationId, e.PresentationId });
            builder.Entity<SimulationPresentation>()
                .HasOne(e => e.Simulation)
                .WithMany(e => e.SimulationPresentations)
                .HasForeignKey(e => e.SimulationId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<SimulationPresentation>()
                .HasOne(e => e.Presentation)
                .WithMany(e => e.SimulationPresentationBackRef)
                .HasForeignKey(e => e.PresentationId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}