using WebApi.Hal;

namespace OpenSim.WebServer.Controllers
{
    public class DynamicRepresentation : Representation
    {
        protected bool AreRelationsEmbedded { get; private set; }

        public void EmbedRelations() => AreRelationsEmbedded = true;
    }
}
