using System.Collections.Generic;
using System.Linq;
using OpenSim.WebServer.App.Model;

namespace OpenSim.WebServer.Model
{
    public class Server
    {
        private ICollection<Simulation> simulations = new List<Simulation>();
        private ICollection<Presentation> presentations = new List<Presentation>();

        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsRunning { get; set; }
        public bool IsCustomUiAvailable { get; set; }
        public User Author { get; set; }

        public IEnumerable<Simulation> Simulations
        {
            get => simulations;
            set => simulations = value.ToList();
        }

        public IEnumerable<Presentation> Presentations
        {
            get => presentations;
            set => presentations = value.ToList();
        }

        public void AddSimulation(Simulation simulation) => simulations.Add(simulation);
        public void AddPresentation(Presentation presentation) => presentations.Add(presentation);
    }
}
