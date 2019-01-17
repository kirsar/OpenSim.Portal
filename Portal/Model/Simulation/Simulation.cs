using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using OpenSim.Portal.Model;

namespace OpenSim.Portal.Model
{
    public class SimulationReference
    {
        public int SimulationId { get; set; }
        public Simulation Simulation { get; set; }

        
        public int ReferenceId { get; set; }
        public Simulation Reference { get; set; }
    }

    public class SimulationConsumer
    {
        public int SimulationId { get; set; }
        public Simulation Simulation { get; set; }


        public int ConsumerId { get; set; }
        public Simulation Consumer { get; set; }
    }

    public class Simulation
    {
        public Simulation() => Presentations = 
            new JoinCollectionFacade<Presentation, Simulation, JoinEntity<Simulation, Presentation>>(this, SimulationPresentations);
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long AuthorId { get; set; }

        public ICollection<SimulationReference> References { get; set; }
        
        private ICollection<JoinEntity<Simulation, Presentation>> SimulationPresentations { get; } = new List<JoinEntity<Simulation, Presentation>>();

        [NotMapped]
        public ICollection<Presentation> Presentations { get; }
    }
}