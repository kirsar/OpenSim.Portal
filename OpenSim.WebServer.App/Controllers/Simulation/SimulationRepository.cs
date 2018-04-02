using System.Collections.Generic;
using System.Collections.Concurrent;

namespace OpenSim.WebServer.App.Controllers.Simulation
{
    public class SimulationRepository : ISimulationRepository
    {
        private ConcurrentDictionary<long, Simulation> simulations = new ConcurrentDictionary<long, Simulation>();
        private int currentId;

        private int GetId() => currentId++;

        public void Add(Simulation simulation)
        {
            var id = GetId();
            simulation.Id = id;
            simulations[id] = simulation;
        }

        public Simulation Get(long id)
        {
            simulations.TryGetValue(id, out var simulation);
            return simulation;
        }

        public IEnumerable<Simulation> GetAll() => simulations.Values;

        public Simulation Remove(long id)
        {
            simulations.TryRemove(id, out var simulation);
            return simulation;
        }

        public void Update(Simulation simulation) => simulations[simulation.Id] = simulation;
    }
}
