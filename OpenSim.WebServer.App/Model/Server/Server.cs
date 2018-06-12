using System.Collections.Generic;
using System.Linq;

namespace OpenSim.WebServer.Model
{
    public class Server
    {
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
        public void AddSimulation(Simulation simulation) => simulations.Add(simulation);

        public IEnumerable<Presentation> Presentations { get; set; }

        private ICollection<Simulation> simulations = new List<Simulation>();
    }
}
