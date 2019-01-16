using System.Linq;
using OpenSim.Portal.Model.User;

namespace OpenSim.Portal.Model.Simulation
{
    public class SimulationRepository : ISimulationRepository
    {
        public SimulationRepository(PortalDbContext context)
        {
            this.context = context;
        }

        public IQueryable<Simulation> GetAll() => context.Simulations;

        public Simulation Get(long id)
        {
            throw new System.NotImplementedException();
        }

        public void Add(Simulation simulation)
        {
            throw new System.NotImplementedException();
        }

        public Simulation Remove(long id)
        {
            throw new System.NotImplementedException();
        }

        public void Update(Simulation simulation)
        {
            throw new System.NotImplementedException();
        }

        private readonly PortalDbContext context;
    }
}