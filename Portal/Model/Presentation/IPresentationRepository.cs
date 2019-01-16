using System.Linq;

namespace OpenSim.Portal.Model.Presentation
{
    public interface IPresentationRepository
    {
        IQueryable<Presentation> GetAll();
        Presentation Get(long id);
        void Add(Presentation simulation);
        Presentation Remove(long id);
        void Update(Presentation simulation);
    }
}
