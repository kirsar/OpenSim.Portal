using System.Collections.Generic;

namespace OpenSim.WebServer.App.Controllers.Simulation
{
    public class Simulation
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public User.User Author { get; set; }
        public IEnumerable<Simulation> SupportedSimulations { get; set; }
    }
}