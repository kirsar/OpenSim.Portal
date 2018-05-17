using System.Collections.Generic;

namespace OpenSim.WebServer.Model
{
    public class Server
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsRunning { get; set; }
        public User Author { get; set; }
        public IEnumerable<Simulation> Simulations { get; set; }
        public IEnumerable<Presentation> Presentations { get; set; }
        public bool IsCustomUiAvailable { get; set; }
    }
}
