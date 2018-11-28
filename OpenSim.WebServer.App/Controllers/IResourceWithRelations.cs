using System.Collections.Generic;
using OpenSim.WebServer.App.Controllers;
using WebApi.Hal.Interfaces;

namespace OpenSim.WebServer.Controllers
{
    public interface IResourceWithRelations : IResource
    {
        void EmbedRelations(FieldsTreeNode embeddedFieldNode, IEmbeddedRelationsSchema schema);
    }
}