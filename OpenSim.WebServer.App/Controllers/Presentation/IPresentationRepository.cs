using System.Collections.Generic;

namespace OpenSim.WebServer.App.Controllers.Presentation
{
    public interface IPresentationRepository
    {
        void Add(Presentation simulation);
        IEnumerable<Presentation> GetAll();
        Presentation Get(long id);
        Presentation Remove(long id);
        void Update(Presentation simulation);
    }
}
