using System.Linq;

namespace OpenSim.Portal.Model
{
    public interface ISimulationRepository
    {
        IQueryable<Simulation> GetAll();
        Simulation Get(int id);
        void Add(Simulation simulation);
        Simulation Remove(int id);
        void Update(Simulation simulation);
    }
}
