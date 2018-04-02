using System.Collections.Generic;

namespace OpenSim.WebServer.App.Controllers.Simulation
{
    public interface ISimulationRepository
    {
        void Add(Simulation simulation);
        IEnumerable<Simulation> GetAll();
        Simulation Get(long id);
        Simulation Remove(long id);
        void Update(Simulation simulation);
    }
}
