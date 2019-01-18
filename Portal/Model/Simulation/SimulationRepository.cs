using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace OpenSim.Portal.Model
{
    public class SimulationRepository : ISimulationRepository
    {
        public SimulationRepository(PortalDbContext context) => this.context = context;

        public IQueryable<Simulation> GetAll() => context.Simulations
            .WithReferences()
            .WithConsumers()
            .WithPresentations();

        public Simulation Get(int id) => context.Simulations.Where(s => s.Id == id)
            .WithReferences()
            .WithConsumers()
            .WithPresentations()
            .SingleOrDefault();

        public void Add(Simulation simulation)
        {
            context.Simulations.Add(simulation);
            context.SaveChanges();
        }

        public Simulation Remove(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Update(Simulation simulation)
        {
            throw new System.NotImplementedException();
        }

        private readonly PortalDbContext context;
    }

    public static class SimulationRepositoryExtensions
    {
        public static IQueryable<Simulation> WithReferences(this IQueryable<Simulation> simulations) => simulations
            .Include(e => e.SimulationReferences)
            .ThenInclude(e => e.Reference);
         
        public static IQueryable<Simulation> WithPresentations(this IQueryable<Simulation> simulations) => simulations
            .Include(e => e.SimulationPresentations)
            .ThenInclude(e => e.Presentation);

        public static IQueryable<Simulation> WithConsumers(this IQueryable<Simulation> simulations) => simulations
            .Include(e => e.SimulationReferencesBackRef)
            .ThenInclude(e => e.Simulation);
    }
}