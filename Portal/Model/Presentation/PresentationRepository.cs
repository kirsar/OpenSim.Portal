using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace OpenSim.Portal.Model
{
    public class PresentationRepository : IPresentationRepository
    {
        public PresentationRepository(PortalDbContext context) => this.context = context;

        private readonly PortalDbContext context;

        public IQueryable<Presentation> GetAll() => context.Presentations
            .WithSimulations();

        public Presentation Get(int id) => context.Presentations.Where(s => s.Id == id)
            .WithSimulations()
            .SingleOrDefault();

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
    }

    public static class PresentationRepositoryExtensions
    {
        public static IQueryable<Presentation> WithSimulations(this IQueryable<Presentation> presentations) => presentations
            .Include(e => e.SimulationPresentationBackRef)
            .ThenInclude(e => e.Simulation);
    }
}