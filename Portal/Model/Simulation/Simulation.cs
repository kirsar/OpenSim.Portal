using System.Collections.Generic;
using System.Linq;

namespace OpenSim.Portal.Model.Simulation
{
    public class Simulation
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public User.User Author { get; set; }
        public IEnumerable<Simulation> References { get; set; } = new List<Simulation>();

        public IList<Simulation> Consumers
        {
            get => consumers;
            set => consumers = value.ToList();
        }

        public void AddConsumer(Simulation consumer) => consumers.Add(consumer);

        public IEnumerable<Presentation.Presentation> Presentations { get; set; } = new List<Presentation.Presentation>();

        private IList<Simulation> consumers = new List<Simulation>();
    }
}