using OpenSim.Portal.Controllers.Presentation;
using OpenSim.Portal.Controllers.Server;
using OpenSim.Portal.Controllers.Simulation;

namespace OpenSim.Portal.Controllers
{
    public interface IEmbeddedRelationsSchema
    {
        ServerEmbeddedRelationSchema Server { get; }
        SimulationEmbeddedRelationSchema Simulation { get; }
        PresentationEmbeddedRelationSchema Presentation { get; }
    }
}