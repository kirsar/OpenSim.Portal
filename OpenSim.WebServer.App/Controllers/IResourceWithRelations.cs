using System.Collections.Generic;
using OpenSim.WebServer.App.Controllers;

namespace OpenSim.WebServer.Controllers
{
    public interface IResourceWithRelations
    {
        void EmbedRelations(IEnumerable<FieldsTreeNode> fields, IEmbeddedRelationsSchema schema);
    }
}