using OpenSim.Portal.Controllers.Presentation;
using OpenSim.Portal.Controllers.Server;
using OpenSim.Portal.Controllers.Simulation;

namespace OpenSim.Portal.Controllers
{

    public class EmbeddedRelationsSchema : IEmbeddedRelationsSchema
    {
        public ServerEmbeddedRelationSchema Server { get; } = new ServerEmbeddedRelationSchema();
        public SimulationEmbeddedRelationSchema Simulation { get; } = new SimulationEmbeddedRelationSchema();
        public PresentationEmbeddedRelationSchema Presentation { get; } = new PresentationEmbeddedRelationSchema();
    }
}