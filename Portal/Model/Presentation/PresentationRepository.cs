using System.Linq;
using OpenSim.Portal.Model;

namespace OpenSim.Portal.Model
{
    public class PresentationRepository : IPresentationRepository
    {
        public PresentationRepository(PortalDbContext context)
        {
            this.context = context;
        }

        private readonly PortalDbContext context;

        public IQueryable<Presentation> GetAll()
        {
            var presentations = context.Presentations;

            // for now fetching other way many to many relation always,
            // but can improve and request only if embedded resource requested
            foreach (var presentation in presentations)
                QuerySimulations(presentation);

            return presentations;
        }

        public Presentation Get(int id)
        {
            var presentation = context.Presentations.Find(id);
            QuerySimulations(presentation);
            return presentation;
        }

        public void Add(Presentation presentation)
        {
            context.Presentations.Add(presentation);
            context.SaveChanges();
        }
        
        public Presentation Remove(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Update(Presentation simulation)
        {
            throw new System.NotImplementedException();
        }

        private void QuerySimulations(Presentation presentation) =>
            presentation.Simulations = context.Simulations.Where(s => s.Presentations.Contains(presentation));
    }
}