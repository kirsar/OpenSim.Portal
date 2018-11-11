using OpenSim.WebServer.App.Controllers;

namespace OpenSim.WebServer.Controllers
{
    public interface IResourceWithRelations
    {
        void EmbedRelations(FieldsTreeNode embeddedFieldNode, IEmbeddedRelationsSchema schema);
    }
}