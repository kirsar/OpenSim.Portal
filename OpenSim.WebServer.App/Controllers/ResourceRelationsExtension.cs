using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using OpenSim.WebServer.App.Controllers;

namespace OpenSim.WebServer.Controllers
{
    internal static class ResourceRelationsExtension
    {
        internal static T EmbedRelations<T>(this T resource, HttpRequest request, IEmbeddedRelationsSchema embeddedRelationsSchema) 
            where T : IResourceWithRelations
        {
            var embeddedNode = request.GetEmbeddedNode();
            if (embeddedNode == null)
                return resource;

            resource.EmbedRelations(embeddedNode, embeddedRelationsSchema);

            return resource;
        }

        internal static List<T> EmbedRelations<T>(this List<T> resources, HttpRequest request, IEmbeddedRelationsSchema embeddedRelationsSchema) 
            where T : IResourceWithRelations
        {
            var embeddedNode = request.GetEmbeddedNode();
            if (embeddedNode == null)
                return resources;

            foreach (var resource in resources)
                resource.EmbedRelations(embeddedNode, embeddedRelationsSchema);

            return resources;
        }

        private static FieldsTreeNode GetEmbeddedNode(this HttpRequest request)
        {
            if (!request.TryGetFields(out var fields))
                return null;

            var fieldsTree = fields.Values.Select(f => f.Parts).UnfoldFieldsTree();
            var resourceNode = fieldsTree.Nodes.Single();
            return resourceNode.GetEmbeddedNode();
        }
    }
}