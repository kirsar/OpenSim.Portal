namespace OpenSim.WebServer.Controllers
{

    public class EmbeddedRelationsSchema : IEmbeddedRelationsSchema
    {
        public ServerEmbeddedRelationSchema Server { get; } = new ServerEmbeddedRelationSchema();
        public SimulationEmbeddedRelationSchema Simulation { get; } = new SimulationEmbeddedRelationSchema();
        public PresentationEmbeddedRelationSchema Presentation { get; } = new PresentationEmbeddedRelationSchema();
    }
}