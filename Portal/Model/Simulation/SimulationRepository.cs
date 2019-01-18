using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace OpenSim.Portal.Model
{
    public class SimulationRepository : ISimulationRepository
    {
        public SimulationRepository(PortalDbContext context)
        {
            this.context = context;
        }

        public IQueryable<Simulation> GetAll() => IncludeRelations(context.Simulations);

        public Simulation Get(int id) => IncludeRelations(context.Simulations).FirstOrDefault(s => s.Id == id);

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

        private IQueryable<Simulation> IncludeRelations(IQueryable<Simulation> simulations)
        {
            simulations
                .Include(e => e.SimulationReferences)
                .ThenInclude(e => e.Reference)
                .Include(e => e.SimulationPresentations)
                .ThenInclude(e => e.Presentation);

            QueryConsumers(simulations);

            return simulations;
        }

        private IQueryable<Simulation> QueryConsumers(IQueryable<Simulation> simulations)
        {
            foreach (var simulation in simulations)
                simulation.Consumers = context.Simulations.Where(s => s.References.Contains(s));

            return simulations;
        }

        private readonly PortalDbContext context;
    }
}