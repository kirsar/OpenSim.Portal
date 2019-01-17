using System.Linq;
using OpenSim.Portal.Model;

namespace OpenSim.Portal.Model
{
    public class SimulationRepository : ISimulationRepository
    {
        public SimulationRepository(PortalDbContext context)
        {
            this.context = context;
        }

        public IQueryable<Simulation> GetAll()
        {
            var simulations = context.Simulations;

            // for now fetching other way many to many relation always,
            // but can improve and request only if embedded resource requested
            foreach (var simulation in simulations)
                QueryConsumers(simulation);

            return simulations;
        }

        public Simulation Get(int id)
        {
            var simulation = context.Simulations.Find(id);
            QueryConsumers(simulation);
            return simulation;
        }

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

        private void QueryConsumers(Simulation simulation) => 
            simulation.Consumers = context.Simulations.Where(s => s.References.Contains(s));

        private readonly PortalDbContext context;
    }
}