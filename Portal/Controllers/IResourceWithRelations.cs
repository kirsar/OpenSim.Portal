using WebApi.Hal.Interfaces;

namespace OpenSim.Portal.Controllers
{
    public interface IResourceWithRelations : IResource
    {
        void EmbedRelations(FieldsTreeNode embeddedFieldNode, IEmbeddedRelationsSchema schema);
    }
}