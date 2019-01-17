using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace OpenSim.Portal.Model
{
    public class Server
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long AuthorId { get; set; }

        private ICollection<ServerSimulation> ServerSimulations { get; } = new List<ServerSimulation>();

        [NotMapped] public IEnumerable<Simulation> Simulations => ServerSimulations.Select(s => s.Simulation);
        public void AddSimulation(Simulation simulation) => ServerSimulations.Add(new ServerSimulation(this, simulation));


        private ICollection<ServerPresentation> ServerPresentations { get; } = new List<ServerPresentation>();

        [NotMapped] public IEnumerable<Presentation> Presentations => ServerPresentations.Select(s => s.Presentation);

        public void AddPresentation(Presentation presentation) => ServerPresentations.Add(new ServerPresentation(this, presentation));
    }

    internal class ServerSimulation
    {
        public ServerSimulation(Server server, Simulation simulation)
        {
            Server = server;
            Simulation = simulation;
        }

        public int ServerId { get; set; }
        public Server Server { get; set; }


        public int SimulationId { get; set; }
        public Simulation Simulation { get; set; }
    }

    internal class ServerPresentation
    {
        public ServerPresentation(Server server, Presentation presentation)
        {
            Server = server;
            Presentation = presentation;
        }

        public int ServerId { get; set; }
        public Server Server { get; set; }


        public int PresentationId { get; set; }
        public Presentation Presentation { get; set; }
    }
}
