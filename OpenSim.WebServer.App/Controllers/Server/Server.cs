using System.Collections.Generic;

namespace OpenSim.WebServer.App.Controllers.Server
{
    public class Server
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsRunning { get; set; }
        public string Description { get; set; }
        public User.User Author { get; set; }
        public IEnumerable<Simulation.Simulation> Simulations { get; set; }
        public IEnumerable<Presentation.Presentation> Presentations { get; set; }
        public bool IsCustomUiAvailable { get; set; }
    }
}
