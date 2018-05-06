using System.Collections.Generic;

namespace OpenSim.WebServer.Model
{
    public class Presentation
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public User Author { get; set; }
        public IEnumerable<Simulation> Simulations { get; set; }
    }
}