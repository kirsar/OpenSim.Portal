using System.Linq;

namespace OpenSim.Portal.Model
{
    public interface IPresentationRepository
    {
        IQueryable<Presentation> GetAll();
        Presentation Get(int id);
        void Add(Presentation presentation);
        Presentation Remove(int id);
        void Update(Presentation simulation);
    }
}
