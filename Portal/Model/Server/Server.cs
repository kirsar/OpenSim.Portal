using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenSim.Portal.Model
{
    public class Server
    {
        public Server()
        {
            Simulations = new JoinCollectionFacade<Simulation, Server, JoinEntity<Server, Simulation>>(this, ServerSimulations);
            Presentations = new JoinCollectionFacade<Presentation, Server, JoinEntity<Server, Presentation>>(this, ServerPresentations);
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long AuthorId { get; set; }

        private ICollection<JoinEntity<Server, Simulation>> ServerSimulations { get; } = new List<JoinEntity<Server, Simulation>>();

        [NotMapped]
        public ICollection<Simulation> Simulations { get; }

        private ICollection<JoinEntity<Server, Presentation>> ServerPresentations { get; } = new List<JoinEntity<Server, Presentation>>();

        [NotMapped]
        public ICollection<Presentation> Presentations { get; }

        public void AddSimulation(Simulation simulation) => Simulations.Add(simulation);
        public void AddPresentation(Presentation presentation) => Presentations.Add(presentation);
    }
}
