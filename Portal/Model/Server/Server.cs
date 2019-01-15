using System.Collections.Generic;
using System.Linq;

namespace OpenSim.Portal.Model.Server
{
    public class Server
    {
        private ICollection<Simulation.Simulation> simulations = new List<Simulation.Simulation>();
        private ICollection<Presentation.Presentation> presentations = new List<Presentation.Presentation>();

        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsRunning { get; set; }
        public bool IsCustomUiAvailable { get; set; }
        public User.User Author { get; set; }

        public IEnumerable<Simulation.Simulation> Simulations
        {
            get => simulations;
            set => simulations = value.ToList();
        }

        public IEnumerable<Presentation.Presentation> Presentations
        {
            get => presentations;
            set => presentations = value.ToList();
        }

        public void AddSimulation(Simulation.Simulation simulation) => simulations.Add(simulation);
        public void AddPresentation(Presentation.Presentation presentation) => presentations.Add(presentation);
    }
}
