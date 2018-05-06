using System.Collections.Generic;

namespace OpenSim.WebServer.Model
{
    public class Simulation
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public User Author { get; set; }
        public IEnumerable<Simulation> References { get; set; }
        public IEnumerable<Simulation> Consumers { get; set; }
        public IEnumerable<Presentation> Presentations { get; set; }
    }
}