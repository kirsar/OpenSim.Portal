namespace OpenSim.WebServer.Controllers
{
    public interface IEmbeddedRelationsSchema
    {
        ServerEmbeddedRelationSchema Server { get; }
        SimulationEmbeddedRelationSchema Simulation { get; }
        PresentationEmbeddedRelationSchema Presentation { get; }
    }
}