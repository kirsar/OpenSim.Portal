using System.Collections.Generic;

namespace OpenSim.Portal.Model.Presentation
{
    public class Presentation
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public User.User Author { get; set; }
        public IEnumerable<Simulation.Simulation> Simulations { get; set; } = new List<Simulation.Simulation>();
    }
}