using System.Collections.Concurrent;
using System.Linq;

namespace OpenSim.Portal.Model.Presentation
{
    public class PresentationRepositoryStub : IPresentationRepository
    {
        private ConcurrentDictionary<long, Presentation> presentations = new ConcurrentDictionary<long, Presentation>();
        private int currentId;

        private int GetId() => currentId++;

        public void Add(Presentation presentation)
        {
            var id = GetId();
            presentation.Id = id;
            presentations[id] = presentation;
        }

        public Presentation Get(long id)
        {
            presentations.TryGetValue(id, out var presentation);
            return presentation;
        }

        public IQueryable<Presentation> GetAll() => presentations.Values.AsQueryable();

        public Presentation Remove(long id)
        {
            presentations.TryRemove(id, out var presentation);
            return presentation;
        }

        public void Update(Presentation presentation) => presentations[presentation.Id] = presentation;
    }
}
