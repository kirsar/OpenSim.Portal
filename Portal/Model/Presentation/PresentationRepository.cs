using System.Linq;
using OpenSim.Portal.Model.User;

namespace OpenSim.Portal.Model.Presentation
{
    public class PresentationRepository : IPresentationRepository
    {
        public PresentationRepository(PortalDbContext context)
        {
            this.context = context;
        }

        private readonly PortalDbContext context;

        public IQueryable<Presentation> GetAll() => context.Presentations;

        public Presentation Get(long id)
        {
            throw new System.NotImplementedException();
        }

        public void Add(Presentation simulation)
        {
            throw new System.NotImplementedException();
        }
        
        public Presentation Remove(long id)
        {
            throw new System.NotImplementedException();
        }

        public void Update(Presentation simulation)
        {
            throw new System.NotImplementedException();
        }
    }
}