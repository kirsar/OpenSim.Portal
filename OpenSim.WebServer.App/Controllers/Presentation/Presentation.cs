using System.Collections.Generic;

namespace OpenSim.WebServer.App.Controllers.Presentation
{
    public class Presentation
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public User.User Author { get; set; }
        public IEnumerable<Simulation.Simulation> SupportedBy { get; set; }
    }
}