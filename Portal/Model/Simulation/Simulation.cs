using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using OpenSim.Portal.Model;

namespace OpenSim.Portal.Model
{
    public class Simulation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long AuthorId { get; set; }

        internal ICollection<SimulationReference> SimulationReferences { get; } = new List<SimulationReference>();
        [NotMapped] public IEnumerable<Simulation> References => SimulationReferences.Select(p => p.Reference);
        public void AddReference(Simulation reference) => SimulationReferences.Add(new SimulationReference(this, reference));

        internal ICollection<SimulationPresentation> SimulationPresentations { get; } = new List<SimulationPresentation>();
        [NotMapped] public IEnumerable<Presentation> Presentations => SimulationPresentations.Select(p => p.Presentation);
        public void AddPresentation(Presentation presentation) => SimulationPresentations.Add(new SimulationPresentation(this, presentation));

        [NotMapped]
        public IEnumerable<Simulation> Consumers { get; set; }

        internal ICollection<ServerSimulation> ServerSimulationsBackRef { get; set; }
        internal ICollection<SimulationReference> SimulationReferencesBackRef { get; set; }
    }

    internal class SimulationReference
    {
        private SimulationReference()
        {
        }

        public SimulationReference(Simulation simulation, Simulation reference)
        {
            Simulation = simulation;
            Reference = reference;
        }

        public int SimulationId { get; set; }
        public Simulation Simulation { get; set; }


        public int ReferenceId { get; set; }
        public Simulation Reference { get; set; }
    }

    internal class SimulationPresentation
    {
        private SimulationPresentation()
        {
        }

        public SimulationPresentation(Simulation simulation, Presentation presentation)
        {
            Simulation = simulation;
            Presentation = presentation;
        }

        public int SimulationId { get; set; }
        public Simulation Simulation { get; set; }


        public int PresentationId { get; set; }
        public Presentation Presentation { get; set; }
    }
}