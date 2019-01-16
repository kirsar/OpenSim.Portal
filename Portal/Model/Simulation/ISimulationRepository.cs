using System.Linq;

namespace OpenSim.Portal.Model.Simulation
{
    public interface ISimulationRepository
    {
        IQueryable<Simulation> GetAll();
        Simulation Get(long id);
        void Add(Simulation simulation);
        Simulation Remove(long id);
        void Update(Simulation simulation);
    }
}
